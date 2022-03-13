using NUnit.Framework;
using System.IO;
using WebGalery.Domain.Databases;
using WebGalery.Domain.Databases.Factories;
using WebGalery.Domain.FileServices;

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
            var rack = factory.CreateDefaultFor(new Database());

            Assert.That(rack.ActiveRootPath.RootPath, Is.EqualTo(Directory.GetCurrentDirectory()));
        }
    }
}
