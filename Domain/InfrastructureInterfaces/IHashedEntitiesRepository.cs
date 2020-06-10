using Domain.Entities;

namespace Domain.InfrastructureInterfaces
{
    public interface IHashedEntitiesRepository
    {
        Hashed GetHashedEntity(string hash);
    }
}