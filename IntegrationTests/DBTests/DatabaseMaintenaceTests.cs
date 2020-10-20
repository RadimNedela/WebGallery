using Domain.Elements.Maintenance;
using Domain.InfrastructureInterfaces;
using Domain.Services;
using Infrastructure.Databases;
using Infrastructure.DomainImpl;
using IntegrationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class DatabaseMaintenaceTests
    {
        private DatabaseInfoElement element;

        [SetUp]
        public void SetUp()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var databaseApplication = serviceProvider.GetService<DatabaseInfoApplication>();
                element = databaseApplication.CreateNewDatabase("TestDatabase");

                databaseApplication.AddNewRack(element.Hash, "NewTestRack", "/mount/ExternalStorage001/Something");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (element != null)
                using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
                {
                    var database = serviceProvider.GetService<IGaleryDatabase>();
                    database.DatabaseInfo.Remove(element.Entity);
                    database.SaveChanges();
                }
        }

        [Test]
        public void CreatedTestMasterData_AreCorrectlyWrittenToDB()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var database = serviceProvider.GetService<IDatabaseInfoEntityRepository>();
                var dbInfo = database.Get(element.Hash);

                Assert.That(dbInfo, Is.Not.Null);
                Assert.That(dbInfo.Name, Is.EqualTo("TestDatabase"));
                var testRack = dbInfo.Racks.First(r => r.Name == "NewTestRack");
                Assert.That(testRack.Name, Is.EqualTo("NewTestRack"));
                Assert.That(testRack.MountPoints.First().Path, Does.Contain("ExternalStorage001"));
            }
        }
    }
}
