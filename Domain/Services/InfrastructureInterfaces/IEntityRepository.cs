using Domain.DbEntities;

namespace Domain.Services.InfrastructureInterfaces
{
    public interface IEntityRepository<T> where T : HashedEntity
    {
        T Get(string hash);

        void Add(T entity);

        void Remove(T entity);

        void Save();
    }
}