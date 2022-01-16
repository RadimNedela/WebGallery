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
            var factory = new RackFactory();
            var rack = factory.CreateDefaultFor(new Database());

            Assert.That(rack.DefaultRootPath.RootPath, Is.EqualTo(Directory.GetCurrentDirectory()));
        }
    }
}
