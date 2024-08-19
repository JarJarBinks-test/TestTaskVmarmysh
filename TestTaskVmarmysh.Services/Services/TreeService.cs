using Microsoft.Extensions.Logging;
using TestTaskVmarmysh.Common.Exceptions;
using TestTaskVmarmysh.DataAccess.Entities.TreeEntities;
using TestTaskVmarmysh.DataAccess.Interfaces;
using TestTaskVmarmysh.Services.Entities;
using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.Services.Services
{
    /// <summary>
    /// Service for acces to tree.
    /// <seealso cref="TestTaskVmarmysh.Services.Interfaces.ITreeService"/> successor.
    /// </summary>
    public class TreeService: ITreeService
    {
        private readonly ITreeRepository _repository;
        private readonly ILogger<TreeService> _logger;

        /// <summary>
        /// Constructore of tree service.
        /// </summary>
        /// <param name="repository">Repository for access to tree.</param>
        /// <param name="logger">Tree service logger.</param>
        public TreeService(ITreeRepository repository, ILogger<TreeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<TreeNodeView> GetTree(string name, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(GetTree)}. {nameof(name)}={name}.");

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new WrongParameterException(nameof(name));
            }

            var result = await _repository.GetTree(name, token);
            if (result == null)
            {
                throw new Exception($"Tree '{name}' not found.");
            }

            TreeNodeView ConvertToViewRecursive(TreeNode node)
            {
                return new TreeNodeView()
                {
                    Id = node.Id,
                    Name = node.Name,
                    Children = node.Children.Select(childNode => ConvertToViewRecursive(childNode)).ToList(),
                };
            };

            var resultViewItems = ConvertToViewRecursive(result);
            return resultViewItems;
        }

        /// <inheritdoc />
        public Task Create(int parentNodeId, string nodeName, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(Create)}. {nameof(parentNodeId)}={parentNodeId}, {nameof(nodeName)}={nodeName}.");

            if (parentNodeId <= 0)
            {
                throw new WrongParameterException(nameof(parentNodeId));
            }
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new WrongParameterException(nameof(nodeName));
            }

            return _repository.Create(parentNodeId, nodeName, token);
        }

        /// <inheritdoc />
        public Task Rename(int nodeId, string newNodeName, CancellationToken token)
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

            return _repository.Rename(nodeId, newNodeName, token);
        }

        /// <inheritdoc />
        public Task Delete(int nodeId, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(Delete)}. {nameof(nodeId)}={nodeId}.");

            if (nodeId <= 0)
            {
                throw new WrongParameterException(nameof(nodeId));
            }

            return _repository.Delete(nodeId, token);
        }
    }
}
