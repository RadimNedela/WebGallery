using Application.Databases;
using ApplicationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using WebGalery.Database.Databases;

namespace ApplicationTests.Databases
{
    [TestFixture]
    public class DatabaseApplicationTests
    {
        [Test]
        public void ResolveFromDI_InitializesAllDependencies()
        {
            var application = new TestFixture().GetService();
            Assert.NotNull(application);
        }

        [Test]
        public void CreateDatabase_ValidDto_CallsSaveChanges()
        {
            var fixture = new TestFixture();
            var application = fixture.WithDatabaseMock().Build();

            application.CreateDatabase(fixture.BuildDto());

            fixture.GaleryDatabase.Received().SaveChanges();
        }

        [Test]
        public void CreateDatabase_ValidDto_CallsBuildDomain()
        {
            var fixture = new TestFixture();
            var application = fixture.WithDomainBuilderMock().Build();
            var dto = fixture.BuildDto();

            // act
            application.CreateDatabase(dto);

            // assert
            fixture.DomainBuilder.Received().BuildDomain(dto);
        }

        private class TestFixture
        {
            public IGaleryDatabase GaleryDatabase { get; private set; }
            public IDatabaseDomainBuilder DomainBuilder { get; private set; }

            public DatabaseApplication GetService()
            {
                var serviceProvider = StaticInitializer.ServiceCollection.BuildServiceProvider();
                return serviceProvider.GetService<DatabaseApplication>();
            }

            public DatabaseApplication Build()
            {
                var serviceProvider = StaticInitializer.ServiceCollection.BuildServiceProvider();
                DatabaseApplication application = new(
                    GaleryDatabase ?? serviceProvider.GetService<IGaleryDatabase>(),
                    DomainBuilder ?? serviceProvider.GetService<IDatabaseDomainBuilder>()
                    );

                return application;
            }

            public DatabaseDto BuildDto()
            {
                return new DatabaseDto();
            }

            public TestFixture WithDatabaseMock()
            {
                GaleryDatabase = Substitute.For<IGaleryDatabase>();
                return this;
            }

            public TestFixture WithDomainBuilderMock()
            {
                DomainBuilder = Substitute.For<IDatabaseDomainBuilder>();
                return this;
            }
        }
    }
}
