using WebGalery.Core.BinderInterfaces;
using WebGalery.Core.InfrastructureInterfaces;
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
            IContentEntityRepository repository = null;
            IBinder binder = null;

            return new PhysicalFilesParser(directoryMethods, hasher, cdiProvider, repository, binder);
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
