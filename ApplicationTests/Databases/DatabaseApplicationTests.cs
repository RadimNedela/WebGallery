using Application.Databases;
using Application.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using WebGalery.Database.Databases;
using WebGalery.Database.IoC;

namespace ApplicationTests.Databases
{
    [TestFixture]
    public class DatabaseApplicationTests
    {
        [Test]
        public void ResolveFromDI_InitializesAllDependencies()
        {
            var application = new TestFixture().Build();
            Assert.NotNull(application);
        }

        [Test]
        public void CreateDatabase_ValidDto_CallsSaveChanges()
        {
            var application = new TestFixture().WithDatabaseMock().Build();

            application.CreateDatabase(new DatabaseDto());
        }

        private class TestFixture
        {
            public IGaleryDatabase GaleryDatabase { get; private set; }

            public DatabaseApplication Build()
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddApplicationServices();
                var builder = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", false, false);
                serviceCollection.RegisterDatabaseServices(builder.Build());

                var serviceProvider = serviceCollection.BuildServiceProvider();

                var service = serviceProvider.GetService<DatabaseApplication>();

                return service;
            }

            public TestFixture WithDatabaseMock()
            {
                GaleryDatabase = Substitute.For<IGaleryDatabase>();
                return this;
            }
        }
    }
}
