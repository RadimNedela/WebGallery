using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using WebGallery.DatabaseEntities;

namespace Infrastructure.DomainImpl
{
    public abstract class EntitiesRepository<T> where T: class, IDatabaseEntity
    {
        protected readonly IGaleryDatabase GaleryDatabase;

        protected EntitiesRepository(IGaleryDatabase galeryDatabase)
        {
            GaleryDatabase = galeryDatabase;
        }

        protected abstract DbSet<T> TheDbSet { get; }

        public void Add(T entity)
        {
            TheDbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            TheDbSet.Remove(entity);
        }

        public void Save()
        {
            GaleryDatabase.SaveChanges();
        }
    }
}