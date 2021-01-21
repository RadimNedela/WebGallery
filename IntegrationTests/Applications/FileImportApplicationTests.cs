using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Tests;
using WebGalery.FileImport.Application;
using WebGalery.FileImport.Domain;
using WebGalery.Infrastructure.FileServices;
using WebGalery.IntegrationTests.IoC;
using WebGalery.Maintenance.Domain;
using WebGalery.Maintenance.Tests;

namespace FileImportTests.Application
{
    [TestFixture]
    public class FileImportApplicationTests
    {
        public const string TestPicturesPath = @"../../../../TestPictures/Duha";

        [Test]
        public void ParseDirectoryContent_ValidTestPath_Returns5Pictures()
        {
            var application = CreateSUT();
            var content = application.ParseDirectoryContent(Path.Combine(Directory.GetCurrentDirectory(), TestPicturesPath));
            Assert.That(content.Files, Is.EqualTo(5));
        }

        private FileImportApplication CreateSUT()
        {
            CoreTestData ctd = new();
            MaintenanceTestData mtd = new();
            FileImportTestData fitd = new();

            var dbInfoProvider = mtd.CreateCurrentDatabaseInfoProvider();

            RackInfoBuilder rackInfoBuilder;
            PhysicalFilesParser physicalFilesParser = fitd.CreateTestPhysicalFilesParser();
            IContentEntityRepository contentRepository;

            FileImportApplication sut = new(rackInfoBuilder, dbInfoProvider, physicalFilesParser, contentRepository);

            return sut;
        }
    }
}
