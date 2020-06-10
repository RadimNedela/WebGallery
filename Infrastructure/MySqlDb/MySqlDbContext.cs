using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MySqlDb
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<Hashed> Hashed { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=galery;user=galeryAdmin;password=galeryAdminPassword");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hashed>(entity =>
            {
                entity.HasIndex(e => e.Hash).IsUnique();
                // entity.HasKey(e => e.ID);
                entity.Property(e => e.Hash).IsRequired();
            });
            // modelBuilder.Entity<Publisher>(entity =>
            // {
            //     entity.HasKey(e => e.ID);
            //     entity.Property(e => e.Name).IsRequired();
            // });

            // modelBuilder.Entity<Book>(entity =>
            // {
            //     entity.HasKey(e => e.ISBN);
            //     entity.Property(e => e.Title).IsRequired();
            //     entity.HasOne(d => d.Publisher)
            // .WithMany(p => p.Books);
            // });
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
    }
}