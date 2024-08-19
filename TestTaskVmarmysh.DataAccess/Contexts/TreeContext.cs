using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestTaskVmarmysh.DataAccess.Entities.TreeEntities;

namespace TestTaskVmarmysh.DataAccess.Context
{
    /// <summary>
    /// Tree database context.
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext"/> successor.
    /// </summary>
    public class TreeContext : DbContext
    {
        private readonly String _connectionString;

        /// <summary>
        /// Dbset for tree nodes.
        /// </summary>
        public DbSet<TreeNode> TreeNode { get; set; }

        /// <summary>
        /// Conscructor of <seealso cref="TestTaskVmarmysh.DataAccess.Context.TreeContext"/>.
        /// </summary>
        /// <param name="configuration">Configuration provider.</param>
        public TreeContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TreeContextConnectionString")
                ?? throw new Exception($"Connection string for {nameof(TreeContext)} not configured.");
        }

        /// <summary>
        /// Configuration context method.
        /// </summary>
        /// <param name="optionsBuilder">The builder options.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
