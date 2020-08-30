using Application.Directories;
using IntegrationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IntegrationTests.Directories
{
    [TestFixture]
    public class PhysicalFileApplicationTests
    {
        [Test]
        public void GetFileStream_ValidHashFromVisitedDirectory_ReturnsCorrectFileStream()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var application = serviceProvider.GetService<PhysicalFileApplication>();
                Stream content = application.GetStream("e5ea5661183409a1d1bb5a82b5e87cba09106cd3");
                Assert.That(content, Is.Not.Null);
            }
        }

    }
}
