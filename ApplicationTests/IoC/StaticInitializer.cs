using Application.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebGalery.Database.IoC;
using WebGalery.Domain.IoC;

namespace ApplicationTests.IoC
{
    internal static class StaticInitializer
    {
        internal static IServiceCollection ServiceCollection { get; private set; }

        static StaticInitializer()
        {
            ServiceCollection = new ServiceCollection();
            ServiceCollection.RegisterDomainServices();
            ServiceCollection.AddApplicationServices();
            var builder = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", false, false);
            ServiceCollection.RegisterDatabaseServices(builder.Build());
        }
    }
}
