using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using WebGalery.Database.Databases;
using WebGalery.Database.Tests.IoC;
using WebGalery.Domain.Warehouses;
using WebGalery.Domain.Warehouses.Factories;

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
            AddDepository(serviceProvider);
            var db = GetDepository(serviceProvider);
            DeleteDepository(serviceProvider, db);
            Assert.That(db, Is.Not.Null);

        }

        private void DeleteDepository(ServiceProvider serviceProvider, Depository depository)
        {
            using var scope = serviceProvider.CreateScope();

            var instance = serviceProvider.GetService<IGaleryDatabase>();

            instance.Depositories.Remove(depository);

            instance.SaveChanges();
        }

        private Depository GetDepository(ServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var instance = serviceProvider.GetService<IGaleryDatabase>();

            var db = instance.Depositories.FirstOrDefault(di => di.Hash == "Asdf");

            return db;
        }

        private static void AddDepository(ServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var databaseInstance = serviceProvider.GetService<IGaleryDatabase>();
            var depositoryFactory = serviceProvider.GetService<IDepositoryFactory>();

            var depository = depositoryFactory.Build("Asdf");

            databaseInstance.Depositories.Add(depository);

            databaseInstance.SaveChanges();
        }
    }
}
