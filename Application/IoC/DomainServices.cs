using Microsoft.Extensions.DependencyInjection;
using WebGalery.FileImport.Services;

namespace WebGalery.FileImport.IoC
{
    public static class DomainServices
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<DirectoryContentBuilder>();
            //services.AddSingleton<IDatabaseInfoElementRepository, DatabaseInfoMemoryStorage2>();
            //services.AddSingleton<DatabaseInfoBuilder>();
            //services.AddScoped<IPathOptimizer, PathOptimizer>();
            //services.AddScoped<DatabaseInfoProvider>();
            //services.AddScoped<IDatabaseInfoProvider>(s => s.GetService<DatabaseInfoProvider>());
            //services.AddScoped<IDatabaseInfoInitializer>(s => s.GetService<DatabaseInfoProvider>());
            services.AddSingleton<IContentElementsMemoryStorage, ContentElementsMemoryStorage>();
        }
    }
}