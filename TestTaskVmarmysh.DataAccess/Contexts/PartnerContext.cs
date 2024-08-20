using Microsoft.EntityFrameworkCore;
using TestTaskVmarmysh.DataAccess.Entities.JournalEntities;

namespace TestTaskVmarmysh.DataAccess.Context
{
    /// <summary>
    /// Partner database context.
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext"/> successor.
    /// </summary>
    public class PartnerContext : DbContext
    {
        /// <summary>
        /// Dbset for remember me objects.
        /// </summary>
        public DbSet<RememberMe> RememberMe { get; set; }

        /// <summary>
        /// Conscructor of <seealso cref="TestTaskVmarmysh.DataAccess.Context.PartnerContext"/>.
        /// </summary>
        /// <param name="options">Configuration options.</param>
        public PartnerContext(DbContextOptions<PartnerContext> options)
            : base(options)
        {
        }
    }
}
