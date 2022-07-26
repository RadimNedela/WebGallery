using System.Collections.Generic;

namespace WebGalery.Application.Maintenance
{
    public class DatabaseInfoDto
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public List<RackDto> Racks { get; set; }

        public DatabaseInfoDto Initialize(string hash, string name, List<RackDto> racks)
        {
            Hash = hash;
            Name = name;
            Racks = racks;

            return this;
        }
    }
}
