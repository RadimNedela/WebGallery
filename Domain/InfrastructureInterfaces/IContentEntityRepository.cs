using Domain.DbEntities;

namespace Domain.InfrastructureInterfaces
{
    public interface IContentEntityRepository
    {
        ContentEntity Get(string hash);

        void Add(ContentEntity contentEntity);
    }
}