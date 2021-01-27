using Microsoft.Extensions.DependencyInjection;
using WebGalery.Binders.Domain;

namespace WebGalery.Binders.IoC
{
    public static class BindersRegistration
    {
        public static void RegisterBinderServices(this IServiceCollection services)
        {
            services.AddScoped<IBinder, Binder>();
        }
    }
}