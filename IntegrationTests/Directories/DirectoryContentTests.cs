using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using Application.Directories;
using Domain.Dtos;
using IntegrationTests.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebApplication;
using WebApplication.Controllers;

namespace IntegrationTests.Directories
{
    [TestFixture]
    public class DirectoryContentTests
    {
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
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var application = serviceProvider.GetService<DirectoryContentApplication>();
                var content = application.GetDirectoryContent(@"../../../../TestPictures");
                Assert.That(content.Where(el => el.GetType().IsEquivalentTo(typeof(DirectoryInfoDto))).Count(), Is.EqualTo(2));
            }
        }

        [Test]
        public void GetDirectoryContent_ValidTestPath_Returns0Files()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var application = serviceProvider.GetService<DirectoryContentApplication>();
                var content = application.GetDirectoryContent(@"../../../../TestPictures");
                Assert.That(content.Where(el => el.GetType().IsEquivalentTo(typeof(FileInfoDto))).Count(), Is.EqualTo(0));
            }
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
                var application = serviceProvider.GetService<DirectoryContentApplication>();
                var list = application.GetDirectoryContent(@"../../../../TestPictures/Duha").ToArray();
                var first = list.First(dto => dto.FileName.EndsWith("2017-08-20-Duha0383.JPG")) as FileInfoDto;
                var second = list.First(dto => dto.FileName.EndsWith("2017-08-20-Duha0383_2.JPG")) as FileInfoDto;
                Assert.That(first.Checksum, Is.EqualTo(second.Checksum));
                Console.WriteLine($"The checksum is {first.Checksum}");
                string s = "";
                foreach (var dto in list)
                {
                    FileInfoDto fi = dto as FileInfoDto;
                    s += ", " + fi.Checksum;
                }
                Console.WriteLine(s);
            }
            t.Stop();
            Console.WriteLine($"TEst trval {t.Elapsed}");
        }

        private IEnumerable<DirectoryElementDto> RecurseIntoDirectories(ServiceProvider serviceProvider)
        {
            var application = serviceProvider.GetService<DirectoryContentApplication>();
            var content = application.GetDirectoryContent(@"../../../../TestPictures");
            IEnumerable<DirectoryElementDto> list = new List<DirectoryElementDto>();
            foreach (var dir in content)
            {
                list = list.Union(application.GetDirectoryContent(dir.FileName));
            }
            return list;
        }

        [Test]
        public void GetRecursivelyDirectoryContent_All8FilesAreDisplayable()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var list = RecurseIntoDirectories(serviceProvider);
                Assert.That(list.Count(dedto => dedto is FileInfoDto && ((FileInfoDto)dedto).IsDisplayableAsImage), Is.EqualTo(8));
            }
        }
    }
}