using Domain.Entities;

namespace Domain.InfrastructureInterfaces
{
    public interface IHashedEntitiesRepository
    {
        Hashed Get(string hash);

        void Add(Hashed hashed);
    }
}