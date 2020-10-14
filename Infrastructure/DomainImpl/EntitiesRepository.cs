using System.Linq;
using Domain.DbEntities;
using Domain.InfrastructureInterfaces;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainImpl
{
    public abstract class EntitiesRepository<T> where T: HashedContentBaseEntity
    {
        protected readonly IGaleryDatabase _galeryDatabase;

        public EntitiesRepository(IGaleryDatabase galeryDatabase)
        {
            _galeryDatabase = galeryDatabase;
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
            _galeryDatabase.SaveChanges();
        }
    }
}