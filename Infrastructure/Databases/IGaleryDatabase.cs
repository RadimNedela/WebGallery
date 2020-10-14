using Domain.DbEntities;
using Domain.DbEntities.Maintenance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases
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