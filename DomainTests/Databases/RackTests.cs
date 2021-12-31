using NUnit.Framework;
using System.IO;
using WebGalery.Domain.Databases.Factories;

namespace DomainTests.Databases
{
    [TestFixture]
    public class RackTests
    {
        [Test]
        public void CreateNew_UsesCurrentDirectory()
        {
            var factory = new RackFactory();
            var rack = factory.Create();

            Assert.That(rack.ActivePath, Is.EqualTo(Directory.GetCurrentDirectory()));
        }
    }
}
