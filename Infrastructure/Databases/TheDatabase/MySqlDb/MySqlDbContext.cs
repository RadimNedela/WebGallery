using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace WebGalery.Infrastructure.Databases.TheDatabase.MySqlDb
{
    public class MySqlDbContext : GaleryDatabase, IGaleryDatabase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost;database=galery;user=galeryAdmin;password=galeryAdminPassword";
            optionsBuilder.EnableSensitiveDataLogging()
                .UseLoggerFactory(LoggerFactory)
                //.UseMySQL("server=localhost;database=galery;user=galeryAdmin;password=galeryAdminPassword");
                .UseMySql(connectionString,
                    new MySqlServerVersion(ServerVersion.AutoDetect(connectionString)), // use MariaDbServerVersion for MariaDB
                        mySqlOptions => mySqlOptions
                            .CharSetBehavior(CharSetBehavior.NeverAppend));
        }
    }
}