using System.Collections.Generic;

namespace Domain.DbEntities.Maintenance
{
    public class DatabaseInfoEntity : HashedEntity
    {
        public string Name { get; set; }
        public List<RackEntity> Racks { get; set; }
        public List<MountPointEntity> MountPoints { get; set; }
    }
}
