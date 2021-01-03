using NUnit.Framework;
using System.Linq;

namespace WebGalery.Core.Tests
{
    [TestFixture]
    public class TestData
    {
        [Test]
        public void DatabaseInfo_TestData_AreConstructedWell()
        {
            var dbInfo = new MaintenanceTestData().TestDatabase;

            Assert.That(dbInfo.Racks.Count, Is.GreaterThan(1), "Test database should contain at least 2 racks, add them...");
            foreach (var rack in dbInfo.Racks)
            {
                Assert.That(rack.MountPoints.Any(), $"Rack {rack} does not contain any mount points");
            }
        }
    }
}
