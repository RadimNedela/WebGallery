using Domain.DbEntities.Maintenance;
using System.Collections.Generic;

namespace Domain.InfrastructureInterfaces
{
    public interface IDatabaseInfoEntityRepository : IEntityRepository<DatabaseInfoEntity>
    {
        IEnumerable<DatabaseInfoEntity> GetAll();
    }
}
