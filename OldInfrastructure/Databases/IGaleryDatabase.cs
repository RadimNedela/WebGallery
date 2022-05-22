using Microsoft.EntityFrameworkCore;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.Maintenance;

namespace WebGalery.Infrastructure.Databases
{
    public interface IGaleryDatabase
    {
        DbSet<Content> Contents { get; }
        DbSet<Binder> Binders { get; }
        DbSet<DatabaseInfo> DatabaseInfo { get; }
        int SaveChanges();
        void DetachAllEntities();
    }
}