namespace WebGalery.Database.Databases
{
    public interface IGaleryDatabase
    {
        //DbSet<Content> Contents { get; }
        //DbSet<Binder> Binders { get; }
        //DbSet<DatabaseInfo> DatabaseInfo { get; }
        int SaveChanges();
        void DetachAllEntities();
    }
}