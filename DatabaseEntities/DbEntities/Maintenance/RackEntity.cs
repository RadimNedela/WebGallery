using System.Collections.Generic;

namespace WebGalery.Core.DbEntities.Maintenance
{
    public class RackEntity : IDatabaseEntity
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public DatabaseInfoEntity Database { get; set; }
        public string DatabaseHash { get; set; }
        public List<MountPointEntity> MountPoints { get; set; }
    }
}
