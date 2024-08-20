using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        /// <param name="services">Services collection.</param>
        /// <param name="configuration">Configuration.</param>
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TreeContext>((ob) => {
                ob.UseSqlServer(configuration.GetConnectionString("TreeContextConnectionString") ?? throw new Exception("TreeContextConnectionString not defined"));
            });
            services.AddDbContext<JournalContext>((ob) => {
                ob.UseSqlServer(configuration.GetConnectionString("JournalContextConnectionString") ?? throw new Exception("JournalContextConnectionString not defined"));
            });
            services.AddDbContext<PartnerContext>((ob) => {
                ob.UseSqlServer(configuration.GetConnectionString("PartnerContextConnectionString") ?? throw new Exception("PartnerContextConnectionString not defined"));
            });

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
