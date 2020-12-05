using System.Collections.Generic;
using Domain.DbEntities.Maintenance;

namespace Domain.Services.InfrastructureInterfaces
{
    public interface IDatabaseInfoEntityRepository : IEntityRepository<DatabaseInfoEntity>
    {
        IEnumerable<DatabaseInfoEntity> GetAll();
    }
}
