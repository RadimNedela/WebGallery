using Domain.DbEntities;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainImpl
{
    public abstract class EntitiesRepository<T> where T: HashedEntity
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