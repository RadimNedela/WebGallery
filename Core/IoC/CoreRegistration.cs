using Microsoft.Extensions.DependencyInjection;
using WebGalery.Binders.Domain;
using WebGalery.Core.BinderInterfaces;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.FileImport.Domain;
using WebGalery.Maintenance.Domain;

namespace WebGalery.Binders.IoC
{
    public static class CoreRegistration
    {
        public static void RegisterCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IBinder, Binder>();
            services.AddScoped<PhysicalFilesParser>();
            services.AddScoped<RackInfoBuilder>();
            //services.AddSingleton<IDatabaseInfoElementRepository, DatabaseInfoMemoryStorage2>();
            //services.AddSingleton<DatabaseInfoBuilder>();
            //services.AddScoped<IPathOptimizer, PathOptimizer>();
            //services.AddSingleton<IContentElementsMemoryStorage, ContentElementsMemoryStorage>();
            services.AddScoped<ICurrentDatabaseInfoProvider, CurrentDatabaseInfoProvider>();
        }
    }
}