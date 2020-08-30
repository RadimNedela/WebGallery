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
                Assert.That(content.Binders.Count(), Is.EqualTo(2));
            }
        }

        [Test]
        public void GetDirectoryContent_ValidTestPath_Returns0Files()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var application = serviceProvider.GetService<DirectoryContentApplication>();
                var content = application.GetDirectoryContent(@"../../../../TestPictures");
                Assert.That(content.Contents.Count(), Is.EqualTo(0));
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
                var list = application.GetDirectoryContent(@"../../../../TestPictures/Duha").Contents.ToArray();
                var first = list.First(dto => dto.Label.EndsWith("2017-08-20-Duha0383.JPG"));
                var second = list.First(dto => dto.Label.EndsWith("2017-08-20-Duha0383_2.JPG"));
                Assert.That(first.Hash, Is.EqualTo(second.Hash));
                Console.WriteLine($"The hash is {first.Hash}");
                string s = "";
                foreach (var dto in list)
                {
                    s += ", " + dto.Hash;
                }
                Console.WriteLine(s);
            }
            t.Stop();
            Console.WriteLine($"TEst trval {t.Elapsed}");
        }

        private IEnumerable<ContentDto> RecurseIntoDirectories(ServiceProvider serviceProvider)
        {
            var application = serviceProvider.GetService<DirectoryContentApplication>();
            var content = application.GetDirectoryContent(@"../../../../TestPictures");
            IEnumerable<ContentDto> list = new List<ContentDto>();
            foreach (var dir in content.Binders)
            {
                list = list.Union(application.GetDirectoryContent(dir.Label).Contents);
            }
            return list;
        }
    }
}