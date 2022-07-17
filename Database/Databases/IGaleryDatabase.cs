using Microsoft.EntityFrameworkCore;
using WebGalery.Domain.Warehouses;

namespace WebGalery.Database.Databases
{
    public interface IGaleryDatabase
    {
        //DbSet<Content> Contents { get; }
        //DbSet<Binder> Binders { get; }
        DbSet<Depository> Depositories { get; }
        int SaveChanges();
        void DetachAllEntities();
    }
}