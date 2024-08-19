using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskVmarmysh.DataAccess.Entities.TreeEntities
{
    /// <summary>
    /// Tree node item class.
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// Tree node id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Tree node name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Tree node item parent id.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Tree node item parent.
        /// </summary>
        public virtual TreeNode? Parent { get; set; }

        /// <summary>
        /// Children of item.
        /// </summary>
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();
    }
}
