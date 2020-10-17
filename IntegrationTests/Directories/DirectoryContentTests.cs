using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Application.Directories;
using Domain.Dtos;
using Domain.InfrastructureInterfaces;
using Domain.Services;
using Infrastructure.Databases;
using Infrastructure.DomainImpl;
using IntegrationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
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
            var dip = Substitute.For<IPathOptimizer>();
            dip.CreateValidSubpathAccordingToCurrentConfiguration(Arg.Any<string>()).Returns(i => i.ArgAt<string>(0));
            var app = new DirectoryContentApplication(new DirectoryContentBuilder(new DirectoryMethods(), new FileHasher(), new ElementsMemoryStorage(), dip),
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
            var list = RecurseIntoDirectories();
            Assert.That(list.Count(), Is.EqualTo(8));
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

        private IEnumerable<ContentInfoDto> RecurseIntoDirectories()
        {
            var application = GetTestApplication();
            var content = application.GetDirectoryContent(TestPicturesPath);
            IEnumerable<ContentInfoDto> list = new List<ContentInfoDto>();
            foreach (var dir in content.Binders)
            {
                list = list.Union(application.GetDirectoryContent(dir.Label).ContentInfos);
            }
            return list;
        }

        [Test]
        public void TryIncludingDB()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var application = serviceProvider.GetService<DirectoryContentApplication>();
                var contentRepository = serviceProvider.GetService<IContentEntityRepository>();
                var binderRepository = serviceProvider.GetService<IBinderEntityRepository>();

                var directoryContentDto = application.GetDirectoryContent(TestPicturesInnerPath);

                foreach (var contentInfo in directoryContentDto.ContentInfos)
                {
                    var contentEntity = contentRepository.Get(contentInfo.Hash);
                    contentRepository.Remove(contentEntity);
                }
                var binderEntity = binderRepository.Get(directoryContentDto.TheBinder.Hash);
                binderRepository.Remove(binderEntity);

                contentRepository.Save();
            }
        }

        //[Test]
        //public void GetFileStream_ValidHashFromVisitedDirectory_ReturnsCorrectFileStream()
        //{
        //    var dirApp = GetTestApplication();
        //    var dc = dirApp.GetDirectoryContent(TestPicturesInnerPath);
        //    var hash = dc.ContentInfos.First(c => c.FilePath.EndsWith(DoubledPictureName2)).Hash;

        //    var application = serviceProvider.GetService<PhysicalFileApplication>();
        //    var content = application.GetContent(hash);
        //    Assert.That(content, Is.Not.Null);
        //    Assert.That(content.FilePath, Is.Not.Null);
        //}
    }
}