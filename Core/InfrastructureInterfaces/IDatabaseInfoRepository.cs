using System.Collections.Generic;
using WebGalery.Core.InfrastructureInterfaces.Base;
using WebGalery.Core.Maintenance;

namespace WebGalery.Core.InfrastructureInterfaces
{
    public interface IDatabaseInfoRepository : IRepository<DatabaseInfo>
    {
        IEnumerable<DatabaseInfo> GetAll();
    }
}
