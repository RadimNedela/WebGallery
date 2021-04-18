using Microsoft.Extensions.DependencyInjection;
using WebGalery.Core;
using WebGalery.FileImport.Application;
using WebGalery.FileImport.Domain;
using WebGalery.Maintenance.Applications;
using WebGalery.SessionHandling.Applications;

namespace WebGalery.FileImport.IoC
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