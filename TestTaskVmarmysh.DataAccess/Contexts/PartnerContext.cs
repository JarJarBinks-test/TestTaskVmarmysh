using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestTaskVmarmysh.DataAccess.Entities.JournalEntities;

namespace TestTaskVmarmysh.DataAccess.Context
{
    /// <summary>
    /// Partner database context.
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext"/> successor.
    /// </summary>
    public class PartnerContext : DbContext
    {
        private readonly String _connectionString;

        /// <summary>
        /// Dbset for remember me objects.
        /// </summary>
        public DbSet<RememberMe> RememberMe { get; set; }

        /// <summary>
        /// Conscructor of <seealso cref="TestTaskVmarmysh.DataAccess.Context.PartnerContext"/>.
        /// </summary>
        /// <param name="configuration">Configuration provider.</param>
        public PartnerContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PartnerContextConnectionString")
                ?? throw new Exception($"Connection string for {nameof(PartnerContext)} not configured.");
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
