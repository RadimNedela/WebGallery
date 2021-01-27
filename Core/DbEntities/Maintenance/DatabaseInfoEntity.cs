using System.Collections.Generic;

namespace WebGalery.Core.DbEntities.Maintenance
{
    public class DatabaseInfoEntity : IDatabaseEntity
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public List<RackEntity> Racks { get; set; }
    }
}
