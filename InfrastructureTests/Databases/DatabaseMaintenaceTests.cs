using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebGalery.Core.Maintenance;
using WebGalery.Core.Tests;
using WebGalery.Infrastructure.Databases;
using WebGalery.Infrastructure.Repositories;
using WebGalery.Infrastructure.Tests.IoC;

namespace WebGalery.Infrastructure.Tests.Databases
{
    [TestFixture]
    public class DatabaseMaintenaceTests
    {
        private DatabaseInfo _info;
        private string _testDatabaseInfoEntityHash;

        [SetUp]
        public void SetUp()
        {
            _info = new CoreTestData().TestDatabase;
            _testDatabaseInfoEntityHash = _info.Hash;
            AddTestDataToDb();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveTestDataFromDb();
            _info = null;
        }

        private void RemoveTestDataFromDb()
        {
            using var serviceProvider = InfrastructureTestsUtils.CreateServiceCollection().BuildServiceProvider();
            var database = serviceProvider.GetService<IGaleryDatabase>();
            database.DatabaseInfo.Remove(_info);
            database.SaveChanges();
            database.DetachAllEntities();
        }

        private void AddTestDataToDb()
        {
            using var serviceProvider = InfrastructureTestsUtils.ServiceProvider;
            var database = serviceProvider.GetService<IGaleryDatabase>();
            database.DatabaseInfo.Add(_info);
            database.SaveChanges();
            database.DetachAllEntities();
        }

        [Test]
        public void GetMaintenanceData_WillReturnInsertedData()
        {
            using var serviceProvider = InfrastructureTestsUtils.ServiceProvider;
            var database = serviceProvider.GetService<IGaleryDatabase>();
            var repository = new DatabaseInfoRepository(database);

            //Act
            var info = repository.Get(_testDatabaseInfoEntityHash);

            //Assert
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Contains.Substring("Info Test Name"));
            Assert.That(info.Racks.Count, Is.EqualTo(2));
            Assert.That(info.Racks[0].Database, Is.SameAs(info));
            Assert.That(info.Racks[0].DatabaseHash, Contains.Substring(_testDatabaseInfoEntityHash));

            var rackWith2Mountpoints = info.Racks.First(r => r.MountPoints.Count == 2);
            var firstMountpointFromTwo = rackWith2Mountpoints.MountPoints.First();
            Assert.That(firstMountpointFromTwo.Rack, Is.SameAs(rackWith2Mountpoints));
        }
    }
}
