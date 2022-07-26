using DomainTests;
using NUnit.Framework;

namespace WebGalery.Domain.Tests.Warehouses
{
    [TestFixture]
    public class DepositoryTests
    {
        [Test]
        public void CreateNew_2Different_PropertiesAreDifferent()
        {
            var mother = new ObjectMother();
            var factory = mother.DepositoryFactory;
            var depository1 = factory.Build(null);
            var depository2 = factory.Build(null);

            Assert.That(depository1.Name, Is.Not.EqualTo(depository2.Name));
            Assert.That(depository1.Hash, Is.Not.EqualTo(depository2.Hash));
        }

        [Test]
        public void CreateNew_ContainsDefaultRack()
        {
            var mother = new ObjectMother();
            var factory = mother.DepositoryFactory;
            var database = factory.Build(null);

            Assert.That(database.Depots, Is.Not.Empty);
        }
    }
}
