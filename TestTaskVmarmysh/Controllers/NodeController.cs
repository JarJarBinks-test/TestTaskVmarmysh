using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.Controllers
{
    /// <summary>
    /// Controller for nodes methods.
    /// </summary>
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly ITreeService _treeService;
        private readonly ILogger<NodeController> _logger;

        /// <summary>
        /// Constructor of node controller.
        /// </summary>
        /// <param name="treeService">Service for access to tree nodes.</param>
        /// <param name="logger">Nodes controller loger.</param>
        public NodeController(ITreeService treeService, ILogger<NodeController> logger)
        {
            _treeService = treeService;
            _logger = logger;
        }

        /// <summary>
        /// Create tree node.
        /// </summary>
        /// <param name="treeName">Tree name.[Obsoleted. (For compatibility only). Will remove.]</param>
        /// <param name="parentNodeId">Tree node parent id.</param>
        /// <param name="nodeName">Tree node name.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Create task.</returns>
        [HttpPost("api.user.tree.node.create")]
        public Task Create([FromQuery, Required] string treeName, [FromQuery] int? parentNodeId, [FromQuery, Required] string nodeName, CancellationToken token)
        {
            // TODO: Need remove parameter treeName. Not used.
            _logger.LogInformation($"{nameof(Create)}. {nameof(treeName)}={treeName}, {nameof(parentNodeId)}={parentNodeId}, {nameof(nodeName)}={nodeName}.");
            return _treeService.Create(parentNodeId, nodeName, token);
        }

        /// <summary>
        /// Rename tree node.
        /// </summary>
        /// <param name="treeName">Tree name.[Obsoleted. (For compatibility only). Will remove.]</param>
        /// <param name="nodeId">Tree node id.</param>
        /// <param name="newNodeName">Tree node name.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Create task.</returns>
        [HttpPost("api.user.tree.node.rename")]
        public Task Rename([FromQuery, Required] string treeName, [FromQuery, Required] int nodeId, [FromQuery, Required] string newNodeName, CancellationToken token)
        {
            // TODO: Need remove parameter treeName. Not used.
            _logger.LogInformation($"{nameof(Rename)}. {nameof(treeName)}={treeName}, {nameof(nodeId)}={nodeId}, {nameof(newNodeName)}={newNodeName}.");
            return _treeService.Rename(nodeId, newNodeName, token);
        }

        /// <summary>
        /// Delete tree node.
        /// </summary>
        /// <param name="treeName">Tree name.[Obsoleted. (For compatibility only). Will remove.]</param>
        /// <param name="nodeId">Tree node id.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Create task.</returns>
        [HttpPost("api.user.tree.node.delete")]
        public Task Delete([FromQuery, Required] string treeName, [FromQuery, Required] int nodeId, CancellationToken token)
        { 
            // TODO: Need remove parameter treeName. Not used.
            _logger.LogInformation($"{nameof(Delete)}. {nameof(treeName)}={treeName}, {nameof(nodeId)}={nodeId}.");
            return _treeService.Delete(nodeId, token);
        }
    }
}
