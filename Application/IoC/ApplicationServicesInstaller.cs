using Application.Databases;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC
{
    public static class ApplicationServicesInstaller
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<DatabaseApplication>();

            return serviceCollection;
        }
    }
}
