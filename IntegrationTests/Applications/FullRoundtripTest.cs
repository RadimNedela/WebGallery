using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using WebGalery.FileImport.Application;
using WebGalery.IntegrationTests.IoC;

namespace WebGalery.IntegrationTests.Applications
{
    [TestFixture]
    public class FullRoundtripTest
    {
        [Test]
        public void ParseDirectoryContent_FullTest()
        {
            using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
            CheckDBEmpty(serviceProvider);
            ParseDirectory(serviceProvider);
            ClearDBCaches(serviceProvider);
            CheckDBContent(serviceProvider);
            DeleteDBContent(serviceProvider);
        }

        private void CheckDBEmpty(ServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }

        private void ParseDirectory(ServiceProvider serviceProvider)
        {
            var application = serviceProvider.GetService<FileImportApplication>();
        }

        private void ClearDBCaches(ServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }

        private void CheckDBContent(ServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }

        private void DeleteDBContent(ServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
