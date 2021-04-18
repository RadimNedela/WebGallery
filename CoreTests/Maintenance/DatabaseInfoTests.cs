using System.IO;
using NUnit.Framework;
using WebGalery.Core.Tests;
using WebGalery.Maintenance.Domain;

namespace WebGalery.Maintenance.Tests.Domain
{

    [TestFixture]
    public class DatabaseInfoTests
    {
        [Test]
        public void GetActiveDirectory_ReturnsExistingPath()
        {
            var dbInfo = CreateSut();

            var path = dbInfo.CurrentInfo.ActiveRack.ActiveDirectory;

            Assert.That(Directory.Exists(path), $"Path {path} does not exist");
        }

        internal CurrentDatabaseInfoProvider CreateSut()
        {
            CoreTestData mtd = new();
            return new CurrentDatabaseInfoProvider(mtd.CreateTestDatabaseSession(), mtd.CreateTestDatabaseRepositorySubstitute());
        }

    }
}
