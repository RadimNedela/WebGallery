using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebGalery.Database.Databases;
using WebGalery.Database.Tests.IoC;

namespace WebGalery.Database.Tests.MasterData
{
    [TestFixture]
    public class MasterDataTests
    {
        [Test]
        public void NewDatabase_CanBeCreated()
        {
            using var serviceProvider = InfrastructureTestsUtils.CreateServiceCollection().BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();

            var instance = serviceProvider.GetService<IGaleryDatabase>();

            Assert.That(instance?.DatabaseInfos != null);

            instance.DatabaseInfos.Add(new Domain.DBModel.DatabaseInfoDB() { Hash = "asdf", Name = "Name" });

            instance.SaveChanges();
        }
    }
}
