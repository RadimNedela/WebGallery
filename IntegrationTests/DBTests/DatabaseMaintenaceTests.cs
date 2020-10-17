using Domain.Elements.Maintenance;
using Domain.Services;
using Infrastructure.Databases;
using IntegrationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

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
        }
    }
}
