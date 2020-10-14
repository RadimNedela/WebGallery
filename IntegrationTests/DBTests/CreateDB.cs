using Domain.Services;
using Infrastructure.Databases;
using IntegrationTests.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class CeateDatabase
    {
        [Test, Explicit("This is only for creating empty DB tables")]
        public void JustCreateTheDatabase()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var instance = serviceProvider.GetService<IGaleryDatabase>();
                DbContext context = instance as DbContext;
                context.Database.EnsureCreated();
            }
        }

        [Test, Explicit("This is only for creating default masterdata entries")]
        public void JustCreateDefaultDatabaseMasterdata()
        {
            using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
            {
                var databaseApplication = serviceProvider.GetService<DatabaseInfoApplication>();
                databaseApplication.CreateNewDatabase("Default");
            }
        }
    }
}