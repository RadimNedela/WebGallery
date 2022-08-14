using Microsoft.EntityFrameworkCore;
using WebGalery.Domain.Logging;
using WebGalery.Domain.Warehouses;

namespace WebGalery.Database.Databases.TheDatabase
{
    public abstract class GaleryDatabase : DbContext, IGaleryDatabase
    {
        private ISimpleLogger _log;
        private ISimpleLogger Log => _log ??= new MyOwnLog4NetLogger(GetType());

        //public DbSet<Content> Contents { get; set; }
        //public DbSet<Binder> Binders { get; set; }
        public DbSet<Depository> Depositories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            InitWarehouseTables(modelBuilder);

            //InitContentTables(modelBuilder);
        }

        private void InitWarehouseTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Depository>(entity =>
            {
                entity.ToTable("Depository");
                entity.HasKey(di => di.Hash);
                entity.Property(di => di.Hash).HasColumnType("Char(40)");
                entity.HasIndex(di => di.Name).IsUnique();
                entity.HasMany(Depository.DepotsFieldName).WithOne(nameof(Depot.Depository)).HasForeignKey("DepositoryHash");
            });

            modelBuilder.Entity<Depot>(entity =>
            {
                entity.ToTable("Depot");
                entity.HasKey(de => de.Hash);
                entity.Property(de => de.Hash).HasColumnType("Char(40)");
                entity.Property(de => de.Name);
                entity.Property("DepositoryHash").IsRequired();
                //entity.HasMany(re => re.MountPoints).WithOne(mp => mp.RackBase).HasForeignKey(re => re.RackHash);
            });

            //modelBuilder.Entity<MountPointDB>(entity =>
            //{
            //    entity.ToTable("MountPoint");
            //    entity.HasKey(re => re.Hash);
            //    entity.Property(re => re.Hash).HasColumnType("Char(40)");
            //    entity.Property(re => re.Path).HasMaxLength(200);
            //    entity.Property(re => re.RackHash).IsRequired();
            //});
        }

        //private void InitContentTables(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<BinderToContent>(entity =>
        //    {
        //        entity.ToTable("BinderContent");
        //        entity.HasOne(bc => bc.Binder).WithMany(b => b.Contents).HasForeignKey(bc => bc.BinderHash);
        //        entity.HasOne(bc => bc.Content).WithMany(c => c.Binders).HasForeignKey(bc => bc.ContentHash);
        //        entity.HasKey(bc => new { bc.BinderHash, bc.ContentHash });
        //    });

        //    modelBuilder.Entity<AttributedBinderToContent>(entity =>
        //    {
        //        entity.ToTable("Attribute");
        //        entity.HasOne(bc => bc.Binder).WithMany(b => b.AttributedContents).HasForeignKey(bc => bc.BinderHash);
        //        entity.HasOne(bc => bc.Content).WithMany(c => c.AttributedBinders).HasForeignKey(bc => bc.ContentHash);
        //        entity.Property(attr => attr.Attribute).HasMaxLength(250);
        //        entity.HasKey(attr => new { attr.BinderHash, attr.ContentHash, attr.Attribute });
        //    });

        //    modelBuilder.Entity<Content>(entity =>
        //    {
        //        entity.HasKey(ce => ce.Hash);
        //        entity.Property(e => e.Hash).HasColumnType("Char(40)").IsRequired();
        //        entity.ToTable("Content");
        //    });

        //    modelBuilder.Entity<Binder>(entity =>
        //    {
        //        entity.HasKey(e => e.Hash);
        //        entity.Property(e => e.Hash).HasColumnType("Char(40)").IsRequired();
        //        entity.ToTable("Binder");
        //    });
        //}

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