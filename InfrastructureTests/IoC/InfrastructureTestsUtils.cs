using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebGalery.Infrastructure.IoC;

namespace WebGalery.Infrastructure.Tests.IoC
{
    internal static class InfrastructureTestsUtils
    {
        internal static IServiceCollection CreateServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            services.RegisterInfrastructureServices(configuration);

            return services;
        }

        internal static ServiceProvider ServiceProvider => CreateServiceCollection().BuildServiceProvider();
    }
}