using Domain.DbEntities.Maintenance;
using Infrastructure.Databases;
using Infrastructure.DomainImpl;
using IntegrationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebGalery.Core.Tests;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class DatabaseMaintenaceTests
    {
        private DatabaseInfoEntity _info;
        private string _testDatabaseInfoEntityHash;

        [SetUp]
        public void SetUp()
        {
            _info = MaintenanceTestData.CreateTestDatabase();
            _testDatabaseInfoEntityHash = _info.Hash;
            AddTestDataToDB();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveTestDataFromDB();
            _info = null;
        }

        private void RemoveTestDataFromDB()
        {
            using var serviceProvider = InfrastructureTestsUtils.CreateServiceCollection().BuildServiceProvider();
            var database = serviceProvider.GetService<IGaleryDatabase>();
            database.DatabaseInfo.Remove(_info);
            database.SaveChanges();
            database.DetachAllEntities();
        }

        private void AddTestDataToDB()
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
