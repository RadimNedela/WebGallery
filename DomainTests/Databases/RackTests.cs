using NUnit.Framework;
using System.IO;
using WebGalery.Domain.Databases;

namespace DomainTests.Databases
{
    [TestFixture]
    public class RackTests
    {
        [Test]
        public void CreateNew_UsesCurrentDirectory()
        {
            var mother = new ObjectMother();
            var factory = mother.RackFactory;
            var rack = factory.CreateDefaultFor(new Depository());

            Assert.That(rack.ActiveLocation.Name, Is.EqualTo(Directory.GetCurrentDirectory()));
        }
    }
}
