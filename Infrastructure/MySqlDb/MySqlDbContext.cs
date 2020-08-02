using System.Linq;
using Domain.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MySqlDb
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<HashedEntity> Hashes { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=galery;user=galeryAdmin;password=galeryAdminPassword");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LocationEntity>()
                .ToTable("Location");

            modelBuilder.Entity<TagEntity>()
                .ToTable("Tag");
            modelBuilder.Entity<HashedTagEntity>();
            modelBuilder.Entity<LocationTagEntity>();

            modelBuilder.Entity<LocationEntityToTagEntity>()
                .ToTable("LocationTags")
                .HasKey(lt => new { lt.TagId, lt.LocationId });
            modelBuilder.Entity<HashedEntityToTagEntity>()
                .ToTable("HashedTags")
                .HasKey(ht => new { ht.TagId, ht.HashedId });
            modelBuilder.Entity<LocationEntityToHashedEntity>()
                .ToTable("HashedLocations")
                .HasKey(ht => new { ht.LocationId, ht.HashedId });

            modelBuilder.Entity<HashedEntity>(entity =>
            {
                entity.HasIndex(e => e.Hash).IsUnique();
                // entity.HasKey(e => e.ID);
                entity.Property(e => e.Hash).IsRequired();
                entity.ToTable("Hashed");
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