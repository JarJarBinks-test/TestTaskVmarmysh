using Microsoft.EntityFrameworkCore;
using TestTaskVmarmysh.DataAccess.Entities.JournalEntities;

namespace TestTaskVmarmysh.DataAccess.Context
{
    /// <summary>
    /// Journal database context.
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext"/> successor.
    /// </summary>
    public class JournalContext : DbContext
    {
        /// <summary>
        /// Dbset for journal objects.
        /// </summary>
        public DbSet<Journal> Journal { get; set; }

        /// <summary>
        /// Conscructor of <seealso cref="TestTaskVmarmysh.DataAccess.Context.JournalContext"/>.
        /// </summary>
        /// <param name="options">Configuration options.</param>
        public JournalContext(DbContextOptions<JournalContext> options)
            : base(options)
        {
        }
    }
}
