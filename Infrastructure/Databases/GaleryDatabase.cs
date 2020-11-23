using System.Linq;
using System.Reflection;
using Domain.DbEntities;
using Domain.DbEntities.Maintenance;
using Domain.Services.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Databases
{
    public abstract class GaleryDatabase : DbContext
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Information)
                .AddFilter("System", LogLevel.Debug)
                .AddConsole();
        });

        public DbSet<ContentEntity> Contents { get; set; }
        public DbSet<BinderEntity> Binders { get; set; }
        public DbSet<DatabaseInfoEntity> DatabaseInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            InitMasterDataTables(modelBuilder);

            InitContentTables(modelBuilder);
        }

        private void InitMasterDataTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseInfoEntity>(entity =>
            {
                entity.ToTable("DatabaseInfo");
                entity.HasKey(di => di.Hash);
                entity.Property(di => di.Hash).HasColumnType("Char(40)");
                entity.HasIndex(di => di.Name).IsUnique();
            });

            modelBuilder.Entity<RackEntity>(entity =>
            {
                entity.ToTable("Rack");
                entity.HasKey(re => re.Hash);
                entity.Property(re => re.Hash).HasColumnType("Char(40)");
                entity.Property(re => re.DatabaseId).IsRequired();
            });

            modelBuilder.Entity<MountPointEntity>(entity =>
            {
                entity.ToTable("MountPoint");
                entity.HasKey(mpe => new { mpe.RackId, mpe.Path});
                entity.Property(re => re.Path).HasMaxLength(200);
                entity.Property(re => re.RackId).IsRequired();
            });
        }

        private void InitContentTables(ModelBuilder modelBuilder)
        {
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
            Log.Begin(nameof(DetachAllEntities));

            var changedEntriesCopy = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;

            Log.End(nameof(DetachAllEntities));
        }
    }
}