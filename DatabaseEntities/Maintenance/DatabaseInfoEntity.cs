using System.Collections.Generic;
using WebGalery.DatabaseEntities;

namespace Domain.DbEntities.Maintenance
{
    public class DatabaseInfoEntity : IDatabaseEntity
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public List<RackEntity> Racks { get; set; }
    }
}
