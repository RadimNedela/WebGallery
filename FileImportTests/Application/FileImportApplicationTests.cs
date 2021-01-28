using NUnit.Framework;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Tests;
using WebGalery.FileImport.Application;
using WebGalery.FileImport.Domain;

namespace FileImportTests.Application
{
    [TestFixture]
    public class FileImportApplicationTests
    {
        [Test]
        public void ParseDirectoryContent_ValidTestPath_Returns5Pictures()
        {
            var application = CreateSUT();
            var content = application.ParseDirectoryContent(".");
            Assert.That(content.Files, Is.EqualTo(5));
        }

        private FileImportApplication CreateSUT()
        {
            CoreTestData ctd = new();
            FileImportTestData fitd = new();

            var dbInfoProvider = ctd.CreateTestCurrentDatabaseInfoProvider();

            RackInfoBuilder rackInfoBuilder = fitd.CreateTestRackInfoBuilder();
            PhysicalFilesParser physicalFilesParser = fitd.CreateTestPhysicalFilesParser();
            IContentEntityRepository contentRepository = null; // Substitute.For<IContentEntityRepository>();

            FileImportApplication sut = new(rackInfoBuilder, dbInfoProvider, physicalFilesParser, contentRepository);

            return sut;
        }
    }
}
