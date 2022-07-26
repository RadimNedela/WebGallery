using System.Collections.Generic;

namespace WebGalery.Core.Maintenance
{
    public class DatabaseInfo : IPersistable
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public List<Rack> Racks { get; set; }
    }
}
