using NSubstitute;
using NUnit.Framework;
using System.Linq;
using WebGalery.Core.Tests;
using System.Collections.Generic;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Maintenance.Applications;

namespace WebGalery.Maintenance.Tests.Applications
{
    [TestFixture]
    public class DatabaseInfoApplicationTests
    {
        private DatabaseInfoApplication CreateSut()
        {
            IDatabaseInfoEntityRepository repository = Substitute.For<IDatabaseInfoEntityRepository>();
            repository.GetAll().Returns(new List<DatabaseInfoEntity> { MaintenanceTestData.CreateTestDatabase() });
            IDirectoryMethods directoryMethods = Substitute.For<IDirectoryMethods>();
            IHasher hasher = Substitute.For<IHasher>();
            hasher.ComputeRandomStringHash(Arg.Any<string>()).Returns(a => a.ArgAt<string>(0) + " Random Hash");

            return new DatabaseInfoApplication(repository, directoryMethods, hasher);
        }

        [Test]
        public void GetAllDatabases_ReturnsTestDataConvertedToDtos()
        {
            var application = CreateSut();

            var databases = application.GetAllDatabases();

            Assert.That(databases.Any());
            DatabaseInfoDto db = databases.First();
            Assert.That(db.Hash, Contains.Substring("Test Hash"));
            Assert.That(db.Racks.Count, Is.EqualTo(2));

            var firstRack = db.Racks.First(r => r.MountPoints.Contains(@"D:\TEMP"));
            var secondRack = db.Racks.First(r => r.MountPoints.Contains(@"C:\temp"));
            Assert.That(firstRack.MountPoints.Count, Is.EqualTo(1));
            Assert.That(firstRack.Name, Contains.Substring("Second Test Rack"));
            Assert.That(secondRack.MountPoints.Count, Is.EqualTo(2));
            Assert.That(secondRack.Name, Contains.Substring("Rack Test Name"));
        }

        [Test]
        public void CreateNewDatabase_WillReturnNewDatabase()
        {
            var application = CreateSut();
            
            var retVal = application.CreateNewDatabase("New database name");

            Assert.That(retVal.Name, Contains.Substring("New database name"));
            Assert.That(retVal.Hash, Contains.Substring("New database name Random Hash"));
        }

        [Test]
        public void CreateNewDatabase_WillContainDefaultRack()
        {
            var application = CreateSut();

            var rack = application.CreateNewDatabase("New database name").Racks.First();

            Assert.That(rack.Name, Contains.Substring("Default"));
            Assert.That(rack.Hash, Contains.Substring("Default Random Hash"));
        }

        [Test]
        public void UpdateDatabase_MergesTheDto()
        {
            var application = CreateSut();

        }
    }
}
