using TestTaskVmarmysh.Services.Entities;

namespace TestTaskVmarmysh.Services.Interfaces
{
    /// <summary>
    /// Interface for access to tree.
    /// </summary>
    public interface ITreeService
    {
        /// <summary>
        /// Get tree by tree root name.
        /// <param name="name">Tree name.</param>
        /// <param name="token">The cancellation token.</param>
        /// </summary>
        Task<TreeNodeView> GetTree(string name, CancellationToken token);

        /// <summary>
        /// Create tree node.
        /// </summary>
        /// <param name="parentNodeId">Id of parent node.</param>
        /// <param name="nodeName">New tree node name.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Create task.</returns>
        Task Create(int parentNodeId, string nodeName, CancellationToken token);

        /// <summary>
        /// Rename tree node.
        /// </summary>
        /// <param name="nodeId">Id of node for rename.</param>
        /// <param name="newNodeName">New node name.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Rename task.</returns>
        Task Rename(int nodeId, string newNodeName, CancellationToken token);

        /// <summary>
        /// Delete tree node.
        /// </summary>
        /// <param name="nodeId">Id of node for delete.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Delete task.</returns>
        Task Delete(int nodeId, CancellationToken token);
    }
}
