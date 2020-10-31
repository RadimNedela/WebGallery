using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.IoC
{
    public static class DomainServices
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<DirectoryContentBuilder>();
            services.AddSingleton<ElementsMemoryStorage>();
            services.AddSingleton<DatabasesMemoryStorage>();
            services.AddSingleton<DatabaseInfoBuilder>();
            services.AddScoped<IPathOptimizer, PathOptimizer>();
            services.AddScoped<DatabaseInfoProvider>();
            services.AddScoped<IDatabaseInfoProvider>(s => s.GetService<DatabaseInfoProvider>());
            services.AddScoped<IDatabaseInfoInitializer>(s => s.GetService<DatabaseInfoProvider>());
        }
    }
}