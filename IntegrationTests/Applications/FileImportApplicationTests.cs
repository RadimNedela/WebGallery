using NSubstitute;
using NUnit.Framework;
using WebGalery.Application.FileImport;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.FileImport;
using WebGalery.Core.InfrastructureInterfaces.Base;
using WebGalery.Core.Tests;
using WebGalery.Core.Tests.FileImport;

namespace WebGalery.IntegrationTests.Applications
{
    [TestFixture]
    public class FileImportApplicationTests
    {
        [Test]
        public void ParseDirectoryContent_ValidTestPath_Returns5Pictures()
        {
            var application = CreateSut();
            var content = application.ParseDirectoryContent(".");
            Assert.That(content.Files, Is.EqualTo(5));
        }

        private FileImportApplication CreateSut()
        {
            CoreTestData ctd = new();
            FileImportTestData fitd = new();

            var dbInfoProvider = ctd.CreateTestCurrentDatabaseInfoProvider();

            RackInfoBuilder rackInfoBuilder = fitd.CreateTestRackInfoBuilder();
            PhysicalFilesParser physicalFilesParser = Substitute.For<PhysicalFilesParser>();
            IPersister<Content> contentEntityPersister = Substitute.For<IPersister<Content>>();

            var directoryMethods = ctd.CreateTestDirectoryMethods();
            PhysicalFileToContentConverter physicalFileToContentConverter = Substitute.For<PhysicalFileToContentConverter>();
            DirectoryContentThreadInfoFactory directoryContentThreadInfoFactory = Substitute.For<DirectoryContentThreadInfoFactory>();
            FileImportApplication sut = new(
                rackInfoBuilder, 
                dbInfoProvider, 
                physicalFilesParser, 
                contentEntityPersister, 
                directoryMethods, 
                physicalFileToContentConverter,
                directoryContentThreadInfoFactory);

            return sut;
        }
    }
}
