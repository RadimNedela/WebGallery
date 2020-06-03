using System.Collections.Generic;
using System.Linq;
using Application.Directories;
using Domain.Dtos;
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
        private IServiceCollection CreateServiceCollection()
        {
            var confBuilder = new ConfigurationBuilder();
            var startup = new Startup(confBuilder.Build());
            IServiceCollection services = new ServiceCollection();

            startup.ConfigureServices(services);
            services.AddTransient<DirectoriesController>();

            return services;
        }

        [Test]
        public void DirectoriesController_IsResolvable()
        {
            using (var serviceProvider = CreateServiceCollection().BuildServiceProvider())
            {
                var controller = serviceProvider.GetService<DirectoriesController>();
                Assert.IsNotNull(controller);
            }
        }

        [Test]
        public void GetDirectoryContent_ValidTestPath_Returns2Directories()
        {
            using (var serviceProvider = CreateServiceCollection().BuildServiceProvider())
            {
                var application = serviceProvider.GetService<DirectoryContentApplication>();
                var content = application.GetDirectoryContent(@"../../../../TestPictures");
                Assert.That(content.Where(el => el.GetType().IsEquivalentTo(typeof(DirectoryInfoDto))).Count(), Is.EqualTo(2));
            }
        }

        [Test]
        public void GetDirectoryContent_ValidTestPath_Returns0Files()
        {
            using (var serviceProvider = CreateServiceCollection().BuildServiceProvider())
            {
                var application = serviceProvider.GetService<DirectoryContentApplication>();
                var content = application.GetDirectoryContent(@"../../../../TestPictures");
                Assert.That(content.Where(el => el.GetType().IsEquivalentTo(typeof(FileInfoDto))).Count(), Is.EqualTo(0));
            }
        }

        [Test]
        public void GetRecursivelyDirectoryContent_ReturnsAll7Files()
        {
            using (var serviceProvider = CreateServiceCollection().BuildServiceProvider())
            {
                var list = RecurseIntoDirectories(serviceProvider);
                Assert.That(list.Count(), Is.EqualTo(7));
            }
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
        public void GetRecursivelyDirectoryContent_All7FilesAreDisplayable()
        {
            using (var serviceProvider = CreateServiceCollection().BuildServiceProvider())
            {
                var list = RecurseIntoDirectories(serviceProvider);
                Assert.That(list.Count(dedto => dedto is FileInfoDto && ((FileInfoDto)dedto).IsDisplayableAsImage), Is.EqualTo(7));
            }
        }
    }
}