using Infrastructure.MySqlDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class CeateDB
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public class TestDbContext : MySqlDbContext
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