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
            var factory = new RackFactory(new );
            var rack = factory.CreateFor();

            Assert.That(rack.ActivePath.RootPath, Is.EqualTo(Directory.GetCurrentDirectory()));
        }
    }
}
