using Microsoft.Extensions.DependencyInjection;
using Infrastructure.IoC;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.IoC
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