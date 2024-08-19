using Microsoft.Extensions.DependencyInjection;
using TestTaskVmarmysh.DataAccess.Context;
using TestTaskVmarmysh.DataAccess.Interfaces;
using TestTaskVmarmysh.DataAccess.Repositories;

namespace TestTaskVmarmysh.DataAccess
{
    /// <summary>
    /// Static class for extention methods for repositories.
    /// </summary>
    public static class InitRepositories
    {
        /// <summary>
        /// Add repositories and contexts to services collections.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<TreeContext>();
            services.AddDbContext<JournalContext>();
            services.AddDbContext<PartnerContext>();

            services.AddScoped<ITreeRepository, TreeRepository>();
            services.AddScoped<IJournalRepository, JournalRepository>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();
        }

        /// <summary>
        /// Setup repositories.
        /// </summary>
        /// <param name="services">Services provider.</param>
        public static void SetupRepositories(this IServiceProvider services)
        {
            using (var serviceScope = services.CreateScope())
            {
                var treeContext = serviceScope.ServiceProvider.GetRequiredService<TreeContext>();
                treeContext.Database.EnsureCreated();

                var journalContext = serviceScope.ServiceProvider.GetRequiredService<JournalContext>();
                journalContext.Database.EnsureCreated();

                var partnerContext = serviceScope.ServiceProvider.GetRequiredService<PartnerContext>();
                partnerContext.Database.EnsureCreated();
            }
        }
    }
}
