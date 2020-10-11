using Infrastructure.Databases.MySqlDb;
using Infrastructure.Databases.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class CeateDatabase
    {
        [Test, Explicit("This is only for creating empty DB tables")]
        public void JustCreateTheDatabase_SQLServer()
        {
            using (var context = new SqlServerDbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        [Test, Explicit("This is only for creating empty DB tables")]
        public void JustCreateTheDatabase_MySQL()
        {
            using (var context = new MySqlDbContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}