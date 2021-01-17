using Microsoft.EntityFrameworkCore;
using WebGalery.Core.DbEntities;
using WebGalery.Core.InfrastructureInterfaces.Base;
using WebGalery.Infrastructure.Databases;

namespace WebGalery.Infrastructure.Repositories.Base
{
    public abstract class EntitiesRepository<T> : IEntityPersister<T> where T: class, IDatabaseEntity
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