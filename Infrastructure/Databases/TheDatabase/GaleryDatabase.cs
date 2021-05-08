using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.Logging;
using WebGalery.Core.Maintenance;

namespace WebGalery.Infrastructure.Databases.TheDatabase
{
    public abstract class GaleryDatabase : DbContext
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Information)
                .AddFilter("System", LogLevel.Debug)
                .AddConsole();
        });

        public DbSet<Content> Contents { get; set; }
        public DbSet<Binder> Binders { get; set; }
        public DbSet<DatabaseInfo> DatabaseInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            InitMasterDataTables(modelBuilder);

            InitContentTables(modelBuilder);
        }

        private void InitMasterDataTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseInfo>(entity =>
            {
                entity.ToTable("DatabaseInfo");
                entity.HasKey(di => di.Hash);
                entity.Property(di => di.Hash).HasColumnType("Char(40)");
                entity.HasIndex(di => di.Name).IsUnique();
            });

            modelBuilder.Entity<Rack>(entity =>
            {
                entity.ToTable("Rack");
                entity.HasKey(re => re.Hash);
                entity.Property(re => re.Hash).HasColumnType("Char(40)");
                entity.Property(re => re.DatabaseHash).IsRequired();
                entity.HasMany(re => re.MountPoints).WithOne(mp => mp.Rack).HasForeignKey(re => re.RackHash);
            });

            modelBuilder.Entity<MountPoint>(entity =>
            {
                entity.ToTable("MountPoint");
                //entity.HasKey(mpe => new { mpe.RackHash, mpe.Path});
                entity.Property(re => re.Path).HasMaxLength(200);
                entity.Property(re => re.RackHash).IsRequired();
            });
        }

        private void InitContentTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BinderToContent>(entity =>
            {
                entity.ToTable("BinderContent");
                entity.HasOne(bc => bc.Binder).WithMany(b => b.Contents).HasForeignKey(bc => bc.BinderHash);
                entity.HasOne(bc => bc.Content).WithMany(c => c.Binders).HasForeignKey(bc => bc.ContentHash);
                entity.HasKey(bc => new { bc.BinderHash, bc.ContentHash });
            });

            modelBuilder.Entity<AttributedBinderToContent>(entity =>
            {
                entity.ToTable("Attribute");
                entity.HasOne(bc => bc.Binder).WithMany(b => b.AttributedContents).HasForeignKey(bc => bc.BinderHash);
                entity.HasOne(bc => bc.Content).WithMany(c => c.AttributedBinders).HasForeignKey(bc => bc.ContentHash);
                entity.Property(attr => attr.Attribute).HasMaxLength(250);
                entity.HasKey(attr => new { attr.BinderHash, attr.ContentHash, attr.Attribute });
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(ce => ce.Hash);
                entity.Property(e => e.Hash).HasColumnType("Char(40)").IsRequired();
                entity.ToTable("Content");
            });

            modelBuilder.Entity<Binder>(entity =>
            {
                entity.HasKey(e => e.Hash);
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