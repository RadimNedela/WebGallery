using NUnit.Framework;
using System.IO;
using WebGalery.Core.Tests;
using WebGalery.Maintenance.Domain;

namespace WebGalery.Maintenance.Tests.Domain
{
    [TestFixture]
    public class DatabaseInfoTests
    {
        private CurrentDatabaseInfoProvider CreateSut()
        {
            MaintenanceTestData mtd = new();
            return new CurrentDatabaseInfoProvider(mtd.CreateTestDatabaseSession(), mtd.CreateTestDatabaseRepositorySubstitute());
        }

        [Test]
        public void GetActiveDirectory_ReturnsExistingPath()
        {
            var dbInfo = CreateSut();

            var path = dbInfo.CurrentInfo.CurrentRack.ActiveDirectory;

            Assert.That(Directory.Exists(path), $"Path {path} does not exist");
        }
    }
}
