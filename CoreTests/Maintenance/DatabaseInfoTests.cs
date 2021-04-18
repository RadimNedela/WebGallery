using System.IO;
using NUnit.Framework;
using WebGalery.Core.Maintenance;

namespace WebGalery.Core.Tests.Maintenance
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
