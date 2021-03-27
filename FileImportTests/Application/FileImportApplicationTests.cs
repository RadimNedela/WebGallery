using NSubstitute;
using NUnit.Framework;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces.Base;
using WebGalery.Core.Tests;
using WebGalery.FileImport.Application;
using WebGalery.FileImport.Domain;

namespace WebGalery.FileImport.Tests.Application
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
            PhysicalFilesParser physicalFilesParser = fitd.CreateTestPhysicalFilesParser();
            IEntityPersister<ContentEntity> contentEntityPersister = Substitute.For<IEntityPersister<ContentEntity>>();

            FileImportApplication sut = new(rackInfoBuilder, dbInfoProvider, physicalFilesParser, contentEntityPersister);

            return sut;
        }
    }
}
