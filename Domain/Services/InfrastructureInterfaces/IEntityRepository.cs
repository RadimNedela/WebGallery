using WebGalery.DatabaseEntities;

namespace Domain.Services.InfrastructureInterfaces
{
    public interface IEntityRepository<T> where T : IDatabaseEntity
    {
        T Get(string hash);

        void Add(T entity);

        void Remove(T entity);

        void Save();
    }
}