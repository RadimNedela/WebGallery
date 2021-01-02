using Microsoft.Extensions.DependencyInjection;
using WebGalery.Core;
using WebGalery.SessionHandling.Applications;

namespace WebGalery.SessionHandling.IoC
{
    public static class SessionHandlingRegistration
    {
        public static void RegisterSessionHandling(this IServiceCollection services)
        {
            services.AddScoped<DatabaseInfoProvider>();
            services.AddScoped<IGalerySession>(s => s.GetService<DatabaseInfoProvider>());
            services.AddScoped<IDatabaseInfoInitializer>(s => s.GetService<DatabaseInfoProvider>());
        }
    }
}