using Domain.Services;
using IntegrationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class DatabaseMaintenaceTests
    {
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
