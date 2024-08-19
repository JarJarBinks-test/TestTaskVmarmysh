using Microsoft.AspNetCore.Mvc;
using TestTaskVmarmysh.Services.Entities;
using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.Controllers
{
    /// <summary>
    /// Controller for tree methods.
    /// </summary>
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly ITreeService _treeService;
        private readonly ILogger<TreeController> _logger;

        /// <summary>
        /// Constructor of tree controller.
        /// </summary>
        /// <param name="treeService">Service for access to tree.</param>
        /// <param name="logger">Tree controller loger.</param>
        public TreeController(ITreeService treeService, ILogger<TreeController> logger)
        {
            _treeService = treeService;
            _logger = logger;
        }

        /// <summary>
        /// Get tree by tree root name.
        /// </summary>
        /// <param name="treeName">Tree name.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Root tree node.</returns>
        [HttpPost("api.user.tree.get")]
        public Task<TreeNodeView> Get([FromQuery] string treeName, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(Get)}. {nameof(treeName)}={treeName}.");

            return _treeService.GetTree(treeName, token);
        }
    }
}
