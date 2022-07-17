using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using WebGalery.Database.Databases;
using WebGalery.Database.Tests.IoC;

namespace WebGalery.Database.Tests.MasterData
{
    [TestFixture]
    public class MasterDataTests
    {

        [Test]
        [Category("DatabaseTests")]
        public void NewDatabase_CanBeCreated()
        {
            using var serviceProvider = InfrastructureTestsUtils.CreateServiceCollection().BuildServiceProvider();
            AddDatabaseInfo(serviceProvider);
            var db = GetDatabaseInfo(serviceProvider);
            DeleteDatabaseInfo(serviceProvider, db);
            Assert.That(db, Is.Not.Null);

        }

        private void DeleteDatabaseInfo(ServiceProvider serviceProvider, DatabaseInfoDB dbInfo)
        {
            using var scope = serviceProvider.CreateScope();

            var instance = serviceProvider.GetService<IGaleryDatabase>();

            instance.Depositories.Remove(dbInfo);

            instance.SaveChanges();
        }

        private DatabaseInfoDB GetDatabaseInfo(ServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var instance = serviceProvider.GetService<IGaleryDatabase>();

            var db = instance.Depositories.FirstOrDefault(di => di.Hash == "asdf");

            return db;
        }

        private static void AddDatabaseInfo(ServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var instance = serviceProvider.GetService<IGaleryDatabase>();

            Assert.That(instance?.Depositories != null);

            instance.Depositories.Add(new DatabaseInfoDB() { Hash = "asdf", Name = "Name" });

            instance.SaveChanges();
        }
    }
}
