using NSubstitute;
using WebGalery.Core.Binders;
using WebGalery.Core.FileImport;

namespace WebGalery.Core.Tests.FileImport
{
    public class FileImportTestData
    {
        public PhysicalFilesParser CreateTestPhysicalFilesParser()
        {
            var ctd = new CoreTestData();
            var directoryMethods = ctd.CreateTestDirectoryMethods();
            var hasher = ctd.CreateTestHasher();

            var cdiProvider = ctd.CreateTestCurrentDatabaseInfoProvider();
            BinderFactory binderFactory = Substitute.For<BinderFactory>();

            return new PhysicalFilesParser(directoryMethods, hasher, cdiProvider, binderFactory);
        }

        public RackInfoBuilder CreateTestRackInfoBuilder()
        {
            CoreTestData ctd = new();

            var builder = new RackInfoBuilder(
                ctd.CreateTestDatabaseSession(),
                ctd.CreateTestCurrentDatabaseInfoProvider(),
                ctd.CreateTestDirectoryMethods()
                );

            return builder;
        }
    }
}
