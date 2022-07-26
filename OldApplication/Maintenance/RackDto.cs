using System.Collections.Generic;

namespace WebGalery.Application.Maintenance
{
    public class RackDto
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public List<string> MountPoints { get; set; }

        public RackDto Initialize(string hash, string name, List<string> mountPoints)
        {
            Hash = hash;
            Name = name;
            MountPoints = mountPoints;

            return this;
        }
    }
}
