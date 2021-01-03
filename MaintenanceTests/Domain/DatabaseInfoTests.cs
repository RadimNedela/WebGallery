using NUnit.Framework;
using System.IO;
using WebGalery.Core.Tests;
using WebGalery.Maintenance.Domain;

namespace WebGalery.Maintenance.Tests.Domain
{
    [TestFixture]
    public class DatabaseInfoTests
    {
        private DatabaseInfo CreateSut()
        {
            MaintenanceTestData mtd = new();
            return new DatabaseInfo(mtd.CreateTestDatabaseSession(), mtd.CreateTestDatabaseRepositorySubstitute());
        }

        [Test]
        public void GetActiveDirectory_ReturnsExistingPath()
        {
            var dbInfo = CreateSut();

            var path = dbInfo.GetActiveDirectory();

            Assert.That(Directory.Exists(path), $"Path {path} does not exist");
        }
    }
}
