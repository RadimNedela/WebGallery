using Microsoft.Extensions.DependencyInjection;
using WebGalery.Application.FileImport;
using WebGalery.Application.Maintenance;
using WebGalery.Application.SessionHandling;
using WebGalery.Core;

namespace WebGalery.Application.IoC
{
    public static class ApplicationRegistration
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<FileImportApplication>();
            services.AddScoped<DatabaseInfoApplication>();
            services.AddScoped<DatabaseInfoProvider>();
            services.AddScoped<IGalerySession>(s => s.GetService<DatabaseInfoProvider>());
            services.AddScoped<IDatabaseInfoInitializer>(s => s.GetService<DatabaseInfoProvider>());
        }
    }
}