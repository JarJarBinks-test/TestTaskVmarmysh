namespace TestTaskVmarmysh.Services.Entities
{
    /// <summary>
    /// Tree node view.
    /// </summary>
    public class TreeNodeView
    {
        /// <summary>
        /// Id of tree node.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tree node view name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Children items of tree node view.
        /// </summary>
        public List<TreeNodeView> Children { get; set; } = new List<TreeNodeView>();
    }
}
