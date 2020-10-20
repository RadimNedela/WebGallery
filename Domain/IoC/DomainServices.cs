using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.IoC
{
    public static class DomainServices
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<DirectoryContentBuilder, DirectoryContentBuilder>();
            services.AddSingleton<ElementsMemoryStorage, ElementsMemoryStorage>();
            services.AddSingleton<DatabaseInfoBuilder, DatabaseInfoBuilder>();
            services.AddScoped<IPathOptimizer, PathOptimizer>();
        }
    }
}