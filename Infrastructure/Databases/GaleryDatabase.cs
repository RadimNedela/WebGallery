using System.Linq;
using Domain.DbEntities;
using Domain.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Databases
{
    public abstract class GaleryDatabase : DbContext
    {
        protected static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => {
            builder.AddFilter("Microsoft", LogLevel.Information)
                .AddFilter("System", LogLevel.Debug)
                .AddConsole();
        });

        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DbSet<ContentEntity> Contents { get; set; }
        public DbSet<BinderEntity> Binders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinderEntityToContentEntity>()
                .ToTable("BinderContent")
                .HasKey(bc => new { bc.BinderId, bc.ContentId });
            
            modelBuilder.Entity<AttributedBinderEntityToContentEntity>(entity =>
            {
                entity.ToTable("Attribute");
                //entity.HasIndex(bac => new { bac.BinderId, bac.ContentId, bac.Attribute })
                //    .IsUnique();
                entity.Property(attr => attr.Attribute).HasMaxLength(250);
                entity.HasKey(attr => new { attr.BinderId, attr.ContentId, attr.Attribute });
            });

            modelBuilder.Entity<ContentEntity>(entity =>
            {
                entity.HasIndex(e => e.Hash).IsUnique();
                entity.Property(e => e.Hash).HasColumnType("Char(40)").IsRequired();
                entity.ToTable("Content");
            });

            modelBuilder.Entity<BinderEntity>(entity =>
            {
                entity.HasIndex(e => e.Hash).IsUnique();
                entity.Property(e => e.Hash).HasColumnType("Char(40)").IsRequired();
                entity.ToTable("Binder");
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