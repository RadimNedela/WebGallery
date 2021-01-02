using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebGalery.WebApplication;

namespace WebGalery.IntegrationTests.IoC
{
    public static class InitializationHelper
    {
        public static IServiceCollection CreateServiceCollection()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .Build();

            var startup = new Startup(config);
            IServiceCollection services = new ServiceCollection();

            startup.ConfigureServices(services);

            return services;
        }
    }
}