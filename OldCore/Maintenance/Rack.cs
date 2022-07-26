using System.Collections.Generic;

namespace WebGalery.Core.Maintenance
{
    public class Rack : IPersistable
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public DatabaseInfo Database { get; set; }
        public string DatabaseHash { get; set; }
        public List<MountPoint> MountPoints { get; set; }
    }
}
