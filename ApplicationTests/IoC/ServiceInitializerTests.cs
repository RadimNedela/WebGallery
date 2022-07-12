using Application.Databases;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ApplicationTests.IoC
{
    [TestFixture]
    public class ServiceInitializerTests
    {
        [Test]
        public void Resolve_DatabaseApplication_InitializesAllDependencies()
        {
            var serviceProvider = StaticInitializer.ServiceCollection.BuildServiceProvider();
            var application = serviceProvider.GetService<DatabaseApplication>();
            Assert.NotNull(application);
        }
    }
}
