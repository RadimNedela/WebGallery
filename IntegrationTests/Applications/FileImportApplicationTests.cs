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
using WebGalery.FileImport.Application;
using WebGalery.FileImport.Domain;
using WebGalery.IntegrationTests.IoC;
using WebGalery.Maintenance.Domain;

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
            using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
            
            RackInfoBuilder rackInfoBuilder = serviceProvider.GetService<RackInfoBuilder>();
            CurrentDatabaseInfoProvider dbInfoProvider = serviceProvider.GetService<CurrentDatabaseInfoProvider>();
            PhysicalFilesParser directoryContentBuilder = serviceProvider.GetService<PhysicalFilesParser>();
            IContentEntityRepository contentRepository = Substitute.For<IContentEntityRepository>();

            FileImportApplication sut = new(rackInfoBuilder, dbInfoProvider, directoryContentBuilder, contentRepository);

            return sut;
        }
    }
}
