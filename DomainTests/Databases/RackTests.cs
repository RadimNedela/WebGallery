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
            var factory = new RackFactory(new Sha1Hasher());
            var rack = factory.CreateFor(new Database());

            Assert.That(rack.ActivePath.RootPath, Is.EqualTo(Directory.GetCurrentDirectory()));
        }
    }
}
