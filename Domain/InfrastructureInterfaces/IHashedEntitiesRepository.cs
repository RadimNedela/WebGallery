using Domain.DbEntities;

namespace Domain.InfrastructureInterfaces
{
    public interface IHashedEntitiesRepository
    {
        HashedEntity Get(string hash);

        void Add(HashedEntity hashed);
    }
}