using Microsoft.Extensions.DependencyInjection;
using WebGalery.Core.Binders;
using WebGalery.Core.FileImport;
using WebGalery.Core.Maintenance;

namespace WebGalery.Core.IoC
{
    public static class CoreRegistration
    {
        public static void RegisterCoreServices(this IServiceCollection services)
        {
            services.AddScoped<PhysicalFilesParser>();
            services.AddScoped<RackInfoBuilder>();
            //services.AddSingleton<IDatabaseInfoElementRepository, DatabaseInfoMemoryStorage2>();
            //services.AddSingleton<DatabaseInfoBuilder>();
            //services.AddScoped<IPathOptimizer, PathOptimizer>();
            //services.AddSingleton<IContentElementsMemoryStorage, ContentElementsMemoryStorage>();

            services.AddScoped<IBinderFactory, BinderFactory>();
            services.AddScoped<GalerySession>();
            services.AddScoped<IGalerySession>(x => x.GetRequiredService<GalerySession>());
            services.AddScoped<IGalerySessionInitializer>(x => x.GetRequiredService<GalerySession>());
            services.AddScoped<RackService>();
            services.AddScoped<IActiveRackService, ActiveRackService>();
        }
    }
}