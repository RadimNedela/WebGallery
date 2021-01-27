using Microsoft.Extensions.DependencyInjection;
using WebGalery.Binders.Domain;
using WebGalery.Core.BinderInterfaces;

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