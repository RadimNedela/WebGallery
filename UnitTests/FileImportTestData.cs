using WebGalery.Core.Tests;
using WebGalery.FileImport.Domain;
using WebGalery.Maintenance.Tests;

namespace FileImportTests
{
    public class FileImportTestData
    {
        public PhysicalFilesParser CreateTestPhysicalFilesParser()
        {
            var ctd = new CoreTestData();
            var directoryMethods = ctd.CreateTestDirectoryMethods();
            var hasher = ctd.CreateTestHasher();

            var mtd = new MaintenanceTestData();
            var cdiProvider = mtd.CreateTestCurrentDatabaseInfoProvider();

            return new PhysicalFilesParser(directoryMethods, hasher, cdiProvider, null, null);
        }

        public RackInfoBuilder CreateTestRackInfoBuilder()
        {
            CoreTestData ctd = new();
            MaintenanceTestData mtd = new();

            var builder = new RackInfoBuilder(
                ctd.CreateTestDatabaseSession(),
                mtd.CreateTestCurrentDatabaseInfoProvider(),
                ctd.CreateTestDirectoryMethods()
                );

            return builder;
        }
    }
}
