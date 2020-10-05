using System.Linq;
using Domain.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MySqlDb
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<ContentEntity> Contents { get; set; }
        public DbSet<BinderEntity> Binders { get; set; }
        public DbSet<AttributedBinderEntity> AttributedBinders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=galery;user=galeryAdmin;password=galeryAdminPassword");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinderEntityToContentEntity>()
                .ToTable("BinderContent")
                .HasKey(bc => new { bc.BinderId, bc.ContentId });
            
            modelBuilder.Entity<AttributedBinderEntityToContentEntity>(entity =>
            {
                entity.ToTable("AttributedBinderContent");
                //entity.HasIndex(bac => new { bac.BinderId, bac.ContentId, bac.Attribute })
                //    .IsUnique();
                //.HasKey(bac => new { bac.BinderId, bac.ContentId, bac.Attribute });
            });

            modelBuilder.Entity<ContentEntity>(entity =>
            {
                entity.HasIndex(e => e.Hash).IsUnique();
                // entity.HasKey(e => e.ID);
                entity.Property(e => e.Hash).IsRequired();
                entity.ToTable("Content");
            });

            modelBuilder.Entity<BinderEntity>(entity =>
            {
                entity.HasIndex(e => e.Hash).IsUnique();
                // entity.HasKey(e => e.ID);
                entity.Property(e => e.Hash).IsRequired();
                entity.ToTable("Binder");
            });

            modelBuilder.Entity<AttributedBinderEntity>(entity =>
            {
                entity.HasIndex(e => e.Hash).IsUnique();
                // entity.HasKey(e => e.ID);
                entity.Property(e => e.Hash).IsRequired();
                entity.ToTable("AttributedBinder");
            });
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