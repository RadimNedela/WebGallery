using DomainTests;
using NUnit.Framework;
using System.IO;

namespace WebGalery.Domain.Tests.Warehouses
{
    [TestFixture]
    public class DepotTests
    {
        [Test]
        public void CreateNew_UsesCurrentDirectory()
        {
            var mother = new ObjectMother();
            var factory = mother.DepotFactory;
            var rack = factory.BuildDefaultFor(mother.Depository);

            Assert.That(rack.ActiveLocation.Name, Is.EqualTo(Directory.GetCurrentDirectory()));
        }
    }
}
