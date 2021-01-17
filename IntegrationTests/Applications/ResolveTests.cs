using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebGalery.FileImport.Application;
using WebGalery.IntegrationTests.IoC;
using WebGalery.Maintenance.Applications;

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
