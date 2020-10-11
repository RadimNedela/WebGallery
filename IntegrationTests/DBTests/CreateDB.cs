using Infrastructure.Databases.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class CeateDatabase
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public class TestDbContext : SqlServerDbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder
                   .UseLoggerFactory(MyLoggerFactory); // Warning: Do not create a new ILoggerFactory instance each time

                base.OnConfiguring(optionsBuilder);
            }
        }

        [Test, Explicit("This is only for creating empty DB tables")]
        public void JustCreateTheDatabase()
        {
            using (var context = new TestDbContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}