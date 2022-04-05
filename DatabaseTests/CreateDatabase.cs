using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebGalery.Database.Databases;
using WebGalery.Database.Tests.IoC;

namespace WebGalery.Database.Tests
{
    [TestFixture]
    public class CeateDatabase
    {
        [Test]
        [Ignore("This is only for creating empty DB tables")]
        public void JustCreateTheDatabase()
        {
            using var serviceProvider = InfrastructureTestsUtils.CreateServiceCollection().BuildServiceProvider();
            var instance = serviceProvider.GetService<IGaleryDatabase>();
            if (instance is DbContext context)
                context.Database.EnsureCreated();
        }
    }
}