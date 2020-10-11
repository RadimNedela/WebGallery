using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases.MySqlDb
{
    public class MySqlDbContext : GaleryDatabase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging()
                .UseLoggerFactory(LoggerFactory)
                .UseMySQL("server=localhost;database=galery;user=galeryAdmin;password=galeryAdminPassword");
        }
    }
}