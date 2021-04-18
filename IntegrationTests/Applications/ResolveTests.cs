using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebGalery.Application.FileImport;
using WebGalery.Application.Maintenance;
using WebGalery.IntegrationTests.IoC;

namespace WebGalery.IntegrationTests.Applications
{
    [TestFixture]
    public class ResolveTests
    {
        [Test]
        public void DirectoryContentApplication_IsResolvable()
        {
            using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
            var controller = serviceProvider.GetService<FileImportApplication>();
            Assert.IsNotNull(controller);
        }

        [Test]
        public void MaintenanceController_IsResolvable()
        {
            using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
            var controller = serviceProvider.GetService<DatabaseInfoApplication>();
            Assert.IsNotNull(controller);
        }
    }
}
