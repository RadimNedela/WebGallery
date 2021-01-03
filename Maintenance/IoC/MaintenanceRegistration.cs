using Microsoft.Extensions.DependencyInjection;
using WebGalery.Maintenance.Applications;
using WebGalery.Maintenance.Domain;

namespace WebGalery.Maintenance.IoC
{
    public static class MaintenanceRegistration
    {
        public static void RegisterMaintenance(this IServiceCollection services)
        {
            services.AddScoped<DatabaseInfoApplication>();
            services.AddScoped<CurrentDatabaseInfoProvider>();
        }
    }
}