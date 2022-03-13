using NUnit.Framework;
using WebGalery.Domain.Databases.Factories;
using WebGalery.Domain.FileServices;

namespace DomainTests.Databases
{
    [TestFixture]
    public class DatabaseTests
    {
        [Test]
        public void CreateNew_2Different_PropertiesAreDifferent()
        {
            var mother = new ObjectMother();
            var factory = mother.DatabaseFactory;
            var database1 = factory.Create();
            var database2 = factory.Create();

            Assert.That(database1.Name, Is.Not.EqualTo(database2.Name));
            Assert.That(database1.Hash, Is.Not.EqualTo(database2.Hash));
        }

        [Test]
        public void CreateNew_ContainsDefaultRack()
        {
            var mother = new ObjectMother();
            var factory = mother.DatabaseFactory;
            var database = factory.Create();

            Assert.That(database.Racks, Is.Not.Empty);
        }
    }
}
