using Microsoft.Extensions.DependencyInjection;
using WebGalery.FileImport.Application;
using WebGalery.FileImport.Domain;

namespace WebGalery.FileImport.IoC
{
    public static class FileImportRegistration
    {
        public static void RegisterFileImportServices(this IServiceCollection services)
        {
            services.AddScoped<DirectoryContentApplication>();
            services.AddScoped<IDirectoryContentBuilder, DirectoryContentBuilder>();
            //services.AddSingleton<IDatabaseInfoElementRepository, DatabaseInfoMemoryStorage2>();
            //services.AddSingleton<DatabaseInfoBuilder>();
            //services.AddScoped<IPathOptimizer, PathOptimizer>();
            //services.AddSingleton<IContentElementsMemoryStorage, ContentElementsMemoryStorage>();
        }
    }
}