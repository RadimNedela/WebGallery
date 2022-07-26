using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebGalery.Database.IoC;
using WebGalery.Domain.IoC;

namespace WebGalery.Database.Tests.IoC
{
    internal static class InfrastructureTestsUtils
    {
        internal static IServiceCollection CreateServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            services.RegisterDatabaseServices(configuration);
            services.RegisterDomainServices();

            return services;
        }

        internal static ServiceProvider ServiceProvider => CreateServiceCollection().BuildServiceProvider();
    }
}