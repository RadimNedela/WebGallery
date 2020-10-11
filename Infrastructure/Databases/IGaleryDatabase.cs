using Domain.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases
{
    public interface IGaleryDatabase
    {
        DbSet<ContentEntity> Contents { get; }
        DbSet<BinderEntity> Binders { get; }
        int SaveChanges();
        void DetachAllEntities();
    }
}