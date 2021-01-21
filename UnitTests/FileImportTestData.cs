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
            var cdiProvider = mtd.CreateCurrentDatabaseInfoProvider();

            return new PhysicalFilesParser(directoryMethods, hasher, cdiProvider, null, null);
        }
    }
}
