using Microsoft.Extensions.DependencyInjection;
using WebGalery.Application.FileImport;
using WebGalery.Application.Maintenance;
using WebGalery.Core;

namespace WebGalery.Application.IoC
{
    public static class ApplicationRegistration
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<FileImportApplication>();
            services.AddScoped<DatabaseInfoApplication>();
            services.AddScoped<GalerySession>();
            services.AddScoped<IGalerySession>(s => s.GetService<GalerySession>());
            services.AddScoped<IGalerySessionInitializer>(s => s.GetService<GalerySession>());
        }
    }
}