using Microsoft.EntityFrameworkCore;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Infrastructure.Databases
{
    public interface IGaleryDatabase
    {
        DbSet<ContentEntity> Contents { get; }
        DbSet<BinderEntity> Binders { get; }
        DbSet<DatabaseInfoEntity> DatabaseInfo { get; }
        int SaveChanges();
        void DetachAllEntities();
    }
}