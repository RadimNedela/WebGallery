using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication;
using WebApplication.Controllers;

namespace IntegrationTests.IoC
{
    public static class InitializationHelper
    {
        public static IServiceCollection CreateServiceCollection()
        {
            var confBuilder = new ConfigurationBuilder();
            var startup = new Startup(confBuilder.Build());
            IServiceCollection services = new ServiceCollection();

            startup.ConfigureServices(services);
            services.AddTransient<DirectoriesController>();

            return services;
        }


    }
}