using System.Collections.Generic;
using WebGalery.DatabaseEntities;

namespace Domain.DbEntities.Maintenance
{
    public class RackEntity : IDatabaseEntity
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public DatabaseInfoEntity Database { get; set; }
        public List<MountPointEntity> MountPoints { get; set; }
    }
}
