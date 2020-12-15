using Application.Maintenance;
using Domain.Services.InfrastructureInterfaces;
using NSubstitute;
using NUnit.Framework;
using System.Linq;
using WebGalery.Maintenance.Services;
using WebGalery.Core.Tests;

namespace WebGalery.Maintenance.Tests.Applications
{
    [TestFixture]
    public class DatabaseInfoApplicationTests
    {
        private DatabaseInfoApplication CreateSut()
        {
            IDatabaseInfoEntityRepository repository = Substitute.For<IDatabaseInfoEntityRepository>();
            repository.GetAll().Returns(new List { MaintenanceTestData.CreateTestDatabase() });
            DatabaseInfoDtoConverter converter = new DatabaseInfoDtoConverter();
            IDirectoryMethods directoryMethods = Substitute.For<IDirectoryMethods>();

            return new DatabaseInfoApplication(repository, converter, directoryMethods);
        }

        [Test]
        public void GetAllDatabases_ReturnsTestDataConvertedToDtos()
        {
            var application = CreateSut();

            var databases = application.GetAllDatabases();

            Assert.That(databases.Any());
            var db = databases.First();
            Assert.That(db.Hash, Contains.Substring("Test Hash"));
        }
    }
}
