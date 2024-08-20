using Microsoft.Extensions.DependencyInjection;
using TestTaskVmarmysh.Services.Interfaces;
using TestTaskVmarmysh.Services.Services;
using TestTaskVmarmysh.DataAccess;
using Microsoft.Extensions.Configuration;

namespace TestTaskVmarmysh.Services
{
    /// <summary>
    /// Static class for extention methods for service.
    /// </summary>
    public static class InitServices
    {
        /// <summary>
        /// Add services to services collections.
        /// </summary>
        /// <param name="services">Services collection.</param>
        /// <param name="configuration">Configuration.</param>
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddSingleton<IRequestIdStorage, RequestIdStorage>();
            services.AddScoped<IRequestIdService, RequestIdService>();
            services.AddScoped<ITreeService, TreeService>();
            services.AddScoped<IJournalService, JournalService>();
            services.AddScoped<IPartnerService, PartnerService>();
        }

        /// <summary>
        /// Setup services.
        /// </summary>
        /// <param name="services">Services provider.</param>
        public static void SetupServices(this IServiceProvider services)
        {
            services.SetupRepositories();
        }
    }
}
