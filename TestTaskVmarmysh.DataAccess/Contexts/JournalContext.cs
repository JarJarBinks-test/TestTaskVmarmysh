using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestTaskVmarmysh.DataAccess.Entities.JournalEntities;

namespace TestTaskVmarmysh.DataAccess.Context
{
    /// <summary>
    /// Journal database context.
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext"/> successor.
    /// </summary>
    public class JournalContext : DbContext
    {
        private readonly String _connectionString;

        /// <summary>
        /// Dbset for journal objects.
        /// </summary>
        public DbSet<Journal> Journal { get; set; }

        /// <summary>
        /// Conscructor of <seealso cref="TestTaskVmarmysh.DataAccess.Context.JournalContext"/>.
        /// </summary>
        /// <param name="configuration">Configuration provider.</param>
        public JournalContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("JournalContextConnectionString")
                ?? throw new Exception($"Connection string for {nameof(JournalContext)} not configured.");
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
