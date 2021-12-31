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
            var factory = new DatabaseFactory(new Sha1Hasher());
            var database1 = factory.Create();
            var database2 = factory.Create();

            Assert.That(database1.Name, Is.Not.EqualTo(database2.Name));
            Assert.That(database1.Hash, Is.Not.EqualTo(database2.Hash));
        }
    }
}
