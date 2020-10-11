using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases.SqlServer
{
    public class SqlServerDbContext : GaleryDatabase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=galery;user=galeryAdmin;password=galeryAdminPassword");
        }
    }
}