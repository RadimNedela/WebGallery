using NSubstitute;
using WebGalery.Core.Binders;
using WebGalery.Core.FileImport;

namespace WebGalery.Core.Tests.FileImport
{
    public class FileImportTestData
    {
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
