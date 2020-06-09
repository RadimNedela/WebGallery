using Domain.Elements;

namespace Domain.InfrastructureInterfaces
{
    public interface IHashedElementsRepository
    {
        HashedElement GetHashedElement(string hash);
    }
}