using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebGalery.Database.Databases.TheDatabase.SqlServer
{
    public class SqlServerDbContext : GaleryDatabase, IGaleryDatabase
    {
        protected static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Information)
                .AddFilter("System", LogLevel.Debug)
                .AddProvider(new Log4NetProvider(new Log4NetProviderOptions("log4netConfiguration.xml")));
        });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var connectionString = "Data Source=RADIMS;Initial Catalog=galery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(LoggerFactory)
                .UseSqlServer(connectionString/* + ";database=galery"*/);
        }
    }
}