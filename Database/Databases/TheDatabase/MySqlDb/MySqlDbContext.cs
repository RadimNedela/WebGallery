using Microsoft.EntityFrameworkCore;

namespace WebGalery.Database.Databases.TheDatabase.MySqlDb
{
    public class MySqlDbContext : GaleryDatabase, IGaleryDatabase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost;database=galery;user=galeryAdmin;password=galeryAdminPassword";
            optionsBuilder.EnableSensitiveDataLogging()
                //.UseLoggerFactory(LoggerFactory)
                //.UseMySQL("server=localhost;database=galery;user=galeryAdmin;password=galeryAdminPassword");
                .UseMySql(connectionString,
                    new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))
                            // ,mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)
                            );
        }
    }
}