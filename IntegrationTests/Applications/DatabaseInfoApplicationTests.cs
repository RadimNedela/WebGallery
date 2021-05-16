using System.Linq;
using NSubstitute;
using NUnit.Framework;
using WebGalery.Application.Maintenance;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.InfrastructureInterfaces.Base;
using WebGalery.Core.Maintenance;
using WebGalery.Core.Tests;

namespace WebGalery.IntegrationTests.Applications
{
    [TestFixture]
    public class DatabaseInfoApplicationTests
    {
        private DatabaseInfoApplication CreateSut()
        {
            CoreTestData mtd = new();
            IDirectoryMethods directoryMethods = Substitute.For<IDirectoryMethods>();
            IHasher hasher = Substitute.For<IHasher>();
            hasher.ComputeRandomStringHash(Arg.Any<string>()).Returns(a => a.ArgAt<string>(0) + " Random Hash");
            IPersister<DatabaseInfo> persister = Substitute.For<IPersister<DatabaseInfo>>();

            return new DatabaseInfoApplication(mtd.CreateTestDatabaseRepositorySubstitute(), persister, directoryMethods, hasher);
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
        public void UpdateDatabase_ChangedNameFields_ReturnsSameContentDto()
        {
            var application = CreateSut();
            DatabaseInfoDto dto = application.GetAllDatabases().First();

            dto.Name = "New test database name";
            var rack = dto.Racks.First();
            rack.Name = "New test database first rack name";
            rack.MountPoints[0] = @"z:\NewMountPoint";

            var updated = application.UpdateDatabaseNames(dto);
            Assert.That(updated.Name, Is.EqualTo("New test database name"));
            Assert.That(updated.Racks.First().Name, Is.EqualTo("New test database first rack name"));
            Assert.That(updated.Racks.First().MountPoints[0], Is.EqualTo(@"z:\NewMountPoint"));
            Assert.That(updated.Racks.First().MountPoints[1], Is.EqualTo(@"/tmp"), "Second mount point was not changed, it should stay as it was");
        }

        [Test]
        public void CreateNewRack_WillCreateIt()
        {
            var application = CreateSut();
            DatabaseInfoDto dto = application.GetAllDatabases().First();

            var updated = application.AddNewRack(dto);

            Assert.That(updated.Racks.Count, Is.EqualTo(3));
            var rack = updated.Racks.Last();
            Assert.That(rack.Name, Contains.Substring("Default"));
            Assert.That(rack.Hash, Contains.Substring("Default Random Hash"));
        }

        [Test]
        public void AddNewMountPoint_WillAdd()
        {
            var application = CreateSut();
            DatabaseInfoDto dto = application.GetAllDatabases().First();
            var rack = dto.Racks.First();

            var updated = application.AddNewMountPoint(dto.Hash, rack.Hash);

            var updatedRack = updated.Racks.First(r => r.Hash == rack.Hash);
            Assert.That(updatedRack.MountPoints.Count, Is.EqualTo(3));
        }
    }
}
