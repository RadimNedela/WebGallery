using WebGalery.Core.DbEntities;

namespace WebGalery.Core.InfrastructureInterfaces.Base
{
    public interface IEntityPersister<T> where T : IDatabaseEntity
    {
        void Add(T entity);

        void Remove(T entity);

        void Save();
    }
}