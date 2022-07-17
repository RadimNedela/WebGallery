using Microsoft.EntityFrameworkCore;

namespace WebGalery.Database.Databases
{
    public interface IGaleryDatabase
    {
        //DbSet<Content> Contents { get; }
        //DbSet<Binder> Binders { get; }
        DbSet<DatabaseInfoDB> DatabaseInfos { get; }
        int SaveChanges();
        void DetachAllEntities();
    }
}