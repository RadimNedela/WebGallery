using Domain.DbEntities;
using Domain.DbEntities.Maintenance;
using System.Collections.Generic;

namespace Infrastructure.Databases
{
    public interface IGaleryReadDatabase
    {
        IEnumerable<ContentEntity> Contents { get; }
        IEnumerable<BinderEntity> Binders { get; }
        IEnumerable<DatabaseInfoEntity> DatabaseInfo { get; }
    }
}