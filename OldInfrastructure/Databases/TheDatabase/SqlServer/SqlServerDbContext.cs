using Microsoft.EntityFrameworkCore;

namespace WebGalery.Infrastructure.Databases.TheDatabase.SqlServer
{
    public class SqlServerDbContext : GaleryDatabase, IGaleryDatabase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.EnableSensitiveDataLogging()
                .UseLoggerFactory(LoggerFactory)
                
                .UseSqlServer(connectionString + ";database=galery");
        }
    }
}