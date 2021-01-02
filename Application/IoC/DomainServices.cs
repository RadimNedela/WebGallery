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
            services.AddSingleton<IContentElementsMemoryStorage, ContentElementsMemoryStorage>();
        }
    }
}