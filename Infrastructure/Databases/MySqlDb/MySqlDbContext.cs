using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases.MySqlDb
{
    public class MySqlDbContext : GaleryDatabase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=galery;user=galeryAdmin;password=galeryAdminPassword");
        }
    }
}