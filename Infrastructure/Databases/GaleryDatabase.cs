using System.Linq;
using Domain.DbEntities;
using Domain.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Databases
{
    public interface IGaleryDatabase
    {

    }

    public class GaleryDatabase : DbContext
    {
        protected static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => {
            builder.AddFilter("Microsoft", LogLevel.Information)
                .AddFilter("System", LogLevel.Debug)
                .AddConsole();
        });

        private static readonly ISimpleLogger log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DbSet<ContentEntity> Contents { get; set; }
        public DbSet<BinderEntity> Binders { get; set; }
        public DbSet<AttributedBinderEntity> AttributedBinders { get; set; }

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