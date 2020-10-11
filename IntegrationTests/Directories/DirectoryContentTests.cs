using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Timers;
using Application.Directories;
using Domain.Dtos;
using Domain.Elements;
using Domain.Services;
using Infrastructure.Databases;
using Infrastructure.DomainImpl;
using IntegrationTests.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using WebApplication;
using WebApplication.Controllers;

namespace IntegrationTests.Directories
{
    [TestFixture]
    public class DirectoryContentTests
    {
        public const string TestPicturesPath = @"../../../../TestPictures";
        public const string TestPicturesInnerPath = TestPicturesPath + @"/Duha";
        public const string DoubledPictureName1 = "2017-08-20-Duha0383.JPG";
        public const string DoubledPictureName2 = "2017-08-20-Duha0383_2.JPG";


        private DirectoryContentApplication GetTestApplication()
        {
            var app = new DirectoryContentApplication(new DirectoryContentBuilder(new DirectoryMethods(), new FileHasher(), new ElementsMemoryStorage()),
                new ContentEntitiesRepository(Substitute.For<IGaleryDatabase>()));

            return app;
        }
        [Test]
        public void DirectoriesController_IsResolvable()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var controller = serviceProvider.GetService<DirectoriesController>();
                Assert.IsNotNull(controller);
            }
        }

        [Test]
        public void GetDirectoryContent_ValidTestPath_Returns2Directories()
        {
            var application = GetTestApplication();
            var content = application.GetDirectoryContent(TestPicturesPath);
            Assert.That(content.Binders.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetDirectoryContent_ValidTestPath_Returns0Files()
        {
            var application = GetTestApplication();
                var content = application.GetDirectoryContent(TestPicturesPath);
                Assert.That(content.ContentInfos.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetRecursivelyDirectoryContent_ReturnsAll8Files()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var list = RecurseIntoDirectories(serviceProvider);
                Assert.That(list.Count(), Is.EqualTo(8));
            }
        }

        [Test]
        public void SameTestPictures_WithDifferentMetadata_ReturnsSameHash()
        {
            Stopwatch t = new Stopwatch();
            t.Start();
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var directoryContentBuilder = serviceProvider.GetService<DirectoryContentBuilder>();
                var binder = directoryContentBuilder.GetDirectoryContent(TestPicturesInnerPath);
                var first = binder.Contents.First(he => he.Label.EndsWith(DoubledPictureName1));

                Assert.That(first.AttributedBinders.Count(), Is.EqualTo(2));

                Console.WriteLine($"The hash is {first.Hash}");
                string s = "";
                foreach (var dto in binder.Contents)
                {
                    s += ", " + dto.Hash;
                }
                Console.WriteLine(s);
            }
            t.Stop();
            Console.WriteLine($"TEst trval {t.Elapsed}");
        }

        private IEnumerable<ContentInfoDto> RecurseIntoDirectories(ServiceProvider serviceProvider)
        {
            var application = serviceProvider.GetService<DirectoryContentApplication>();
            var content = application.GetDirectoryContent(TestPicturesPath);
            IEnumerable<ContentInfoDto> list = new List<ContentInfoDto>();
            foreach (var dir in content.Binders)
            {
                list = list.Union(application.GetDirectoryContent(dir.Label).ContentInfos);
            }
            return list;
        }

        [Test]
        public void GetFileStream_ValidHashFromVisitedDirectory_ReturnsCorrectFileStream()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var dirApp = serviceProvider.GetService<DirectoryContentApplication>();
                var dc = dirApp.GetDirectoryContent(TestPicturesInnerPath);
                var hash = dc.ContentInfos.First(c => c.FilePath.EndsWith(DoubledPictureName2)).Hash;

                var application = serviceProvider.GetService<PhysicalFileApplication>();
                var content = application.GetContent(hash);
                Assert.That(content, Is.Not.Null);
                Assert.That(content.FilePath, Is.Not.Null);
            }
        }

    }
}