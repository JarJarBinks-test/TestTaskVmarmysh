using Microsoft.EntityFrameworkCore;
using TestTaskVmarmysh.DataAccess.Entities.TreeEntities;

namespace TestTaskVmarmysh.DataAccess.Context
{
    /// <summary>
    /// Tree database context.
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext"/> successor.
    /// </summary>
    public class TreeContext : DbContext
    {
        /// <summary>
        /// Dbset for tree nodes.
        /// </summary>
        public DbSet<TreeNode> TreeNode { get; set; }

        /// <summary>
        /// Conscructor of <seealso cref="TestTaskVmarmysh.DataAccess.Context.TreeContext"/>.
        /// </summary>
        /// <param name="options">Configuration options.</param>
        public TreeContext(DbContextOptions<TreeContext> options)
            : base(options)
        {
        }
    }
}
