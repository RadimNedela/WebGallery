using System.Collections.Generic;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.InfrastructureInterfaces.Base;

namespace WebGalery.Core.InfrastructureInterfaces
{
    public interface IDatabaseInfoEntityRepository : IEntityRepository<DatabaseInfoEntity>
    {
        IEnumerable<DatabaseInfoEntity> GetAll();
    }
}
