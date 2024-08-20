using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTaskVmarmysh.Common.Exceptions;
using TestTaskVmarmysh.DataAccess.Context;
using TestTaskVmarmysh.DataAccess.Entities.TreeEntities;
using TestTaskVmarmysh.DataAccess.Interfaces;

namespace TestTaskVmarmysh.DataAccess.Repositories
{
    /// <summary>
    /// Repository for acces to tree.
    /// <seealso cref="TestTaskVmarmysh.DataAccess.Interfaces.ITreeRepository"/> successor.
    /// </summary>
    public class TreeRepository: ITreeRepository
    {
        private readonly TreeContext _context;
        private readonly ILogger<TreeRepository> _logger;

        /// <summary>
        /// Constructor of <seealso cref="TestTaskVmarmysh.DataAccess.Repositories.TreeRepository"/>
        /// </summary>
        /// <param name="context">Tree nodes database context.</param>
        /// <param name="logger">Tree repository loger.</param>
        public TreeRepository(TreeContext context, ILogger<TreeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<TreeNode> GetTree(string name, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(GetTree)}. {nameof(name)}={name}.");

            var result = await _context.TreeNode.FromSqlRaw(
            @"WITH treeNodes (Id, Name, ParentId) AS (
                SELECT Id, Name, ParentId
                FROM dbo.TreeNode
                WHERE TreeNode.Name = @rootName
                UNION ALL
                SELECT tn.Id, tn.Name, tn.ParentId
                FROM dbo.TreeNode tn
                INNER JOIN treeNodes o 
                    ON o.Id = tn.ParentId
            )
            SELECT id, name, parentId FROM treeNodes", new SqlParameter("@rootName", System.Data.SqlDbType.VarChar) { Value = name })
            .IgnoreAutoIncludes()
            .ToListAsync(token);

            if (!result.Any())
            {
                throw new Exception($"Tree with name '{name}' does not exist.");
            }

            TreeNode ConvertToTreeRecursive(TreeNode node)
            {
                node.Children = result.Where(childItem => childItem.ParentId == node.Id)
                    .Select(childItem => ConvertToTreeRecursive(childItem)).ToList();
                return node;
            };
            var root = result.Find(node => !node.ParentId.HasValue)!;
            return ConvertToTreeRecursive(root);
        }

        /// <inheritdoc />
        public async Task Create(int? parentNodeId, string nodeName, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(Create)}. {nameof(parentNodeId)}={parentNodeId}, {nameof(nodeName)}={nodeName}.");

            if (parentNodeId.HasValue && parentNodeId <= 0)
            {
                throw new WrongParameterException(nameof(parentNodeId));
            }
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new WrongParameterException(nameof(nodeName));
            }

            var newNode = new TreeNode()
            {
                Name = nodeName,
                ParentId = parentNodeId,
            };

            await _context.TreeNode.AddAsync(newNode, token);
            await _context.SaveChangesAsync(token);
        }

        /// <inheritdoc />
        public async Task Rename(int nodeId, string newNodeName, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(Rename)}. {nameof(nodeId)}={nodeId}, {nameof(newNodeName)}={newNodeName}.");

            if (nodeId <= 0)
            {
                throw new WrongParameterException(nameof(nodeId));
            }
            if (string.IsNullOrWhiteSpace(newNodeName))
            {
                throw new WrongParameterException(nameof(newNodeName));
            }

            var itemForRename = await _context.TreeNode.FindAsync(new object[] { nodeId }, token);
            if (itemForRename == null)
            {
                throw new Exception($"The tree node# {nodeId} does not exist.");
            }

            itemForRename.Name = newNodeName;
            _context.TreeNode.Update(itemForRename);
            await _context.SaveChangesAsync(token);
        }

        /// <inheritdoc />
        public async Task Delete(int nodeId, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(Delete)}. {nameof(nodeId)}={nodeId}.");

            if (nodeId <= 0)
            {
                throw new WrongParameterException(nameof(nodeId));
            }

            var itemForRemove = await _context.TreeNode.FindAsync(new object[] { nodeId }, token);
            if (itemForRemove == null)
            {
                throw new Exception($"The tree node# {nodeId} does not exist.");
            }

            var thereAreChildren = await _context.TreeNode.AnyAsync(node => node.ParentId == nodeId, token);
            if (thereAreChildren)
            {
                throw new SecureException();
            }

            _context.TreeNode.Remove(itemForRemove);
            await _context.SaveChangesAsync(token);
        }
    }
}
