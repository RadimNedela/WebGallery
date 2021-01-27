using WebGalery.Core.DbEntities;

namespace WebGalery.Core.InfrastructureInterfaces.Base
{
    public interface IEntityRepository<T> where T : IDatabaseEntity
    {
        T Get(string hash);
    }
}