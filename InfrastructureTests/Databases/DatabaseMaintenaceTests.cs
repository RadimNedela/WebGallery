using Domain.DbEntities.Maintenance;
using Infrastructure.Databases;
using Infrastructure.DomainImpl;
using IntegrationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class DatabaseMaintenaceTests
    {
        public const string TestDatabaseInfoEntityHash = "Test database info hash";
        private DatabaseInfoEntity _info;

        [SetUp]
        public void SetUp()
        {
            AddTestDataToDB();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveTestDataFromDB();
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
            _info = new DatabaseInfoEntity
            {
                Hash = TestDatabaseInfoEntityHash,
                Name = "Test Database info name",
                Racks = new List<RackEntity>()
            };

            RackEntity rack1 = new ()
            {
                Hash = "Test rack entity 1 hash",
                Name = "Test rack entity 1 name",
                MountPoints = new List<MountPointEntity>()
            };
            _info.Racks.Add(rack1);

            RackEntity rack2 = new()
            {
                Hash = "Test rack entity 2 hash",
                Name = "Test rack entity 2 name",
                MountPoints = new List<MountPointEntity>()
            };
            _info.Racks.Add(rack2);

            MountPointEntity mountPoint11 = new()
            {
                Path = @"c:\temp\rack1",
            };
            rack1.MountPoints.Add(mountPoint11);

            MountPointEntity mountPoint21 = new()
            {
                Path = @"c:\temp\rack21",
            };
            rack2.MountPoints.Add(mountPoint21);
            MountPointEntity mountPoint22 = new()
            {
                Path = @"c:\temp\rack22",
            };
            rack2.MountPoints.Add(mountPoint22);

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
            var info = repository.Get(TestDatabaseInfoEntityHash);

            //Assert
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Contains.Substring("info name"));
            Assert.That(info.Racks.Count, Is.EqualTo(2));
            Assert.That(info.Racks[0].Database, Is.SameAs(info));
            Assert.That(info.Racks[0].DatabaseHash, Contains.Substring(TestDatabaseInfoEntityHash));

            var rackWith2Mountpoints = info.Racks.First(r => r.MountPoints.Count == 2);
            var firstMountpointFromTwo = rackWith2Mountpoints.MountPoints.First();
            Assert.That(firstMountpointFromTwo.Rack, Is.SameAs(rackWith2Mountpoints));
        }
    }
}
