using System.Collections.Generic;

namespace Domain.DbEntities.Maintenance
{
    public class RackEntity : HashedEntity
    {
        public string Name { get; set; }
        public DatabaseInfoEntity Database { get; set; }
        public string DatabaseId { get; set; }
        public List<MountPointEntity> MountPoints { get; set; }
    }
}
