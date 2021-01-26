using WebGalery.Core.Tests;
using WebGalery.Maintenance.Domain;

namespace WebGalery.Maintenance.Tests
{
    public class MaintenanceTestData
    {
        public CurrentDatabaseInfoProvider CreateTestCurrentDatabaseInfoProvider()
        {
            CoreTestData mtd = new();
            return new CurrentDatabaseInfoProvider(mtd.CreateTestDatabaseSession(), mtd.CreateTestDatabaseRepositorySubstitute());
        }
    }
}
