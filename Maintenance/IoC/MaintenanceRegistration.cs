using Microsoft.Extensions.DependencyInjection;
using WebGalery.Maintenance.Applications;

namespace WebGalery.Maintenance.IoC
{
    public static class MaintenanceRegistration
    {
        public static void RegisterMaintenance(this IServiceCollection services)
        {
            services.AddScoped<DatabaseInfoApplication>();
        }
    }
}