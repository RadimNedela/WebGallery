using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebGalery.Infrastructure.Databases;
using WebGalery.Infrastructure.Tests.IoC;

namespace WebGalery.Infrastructure.Tests.Databases
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
            DbContext context = instance as DbContext;
            context.Database.EnsureCreated();
        }
    }
}