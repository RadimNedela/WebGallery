using System.Collections.Generic;
using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Core.Tests
{
    public class DatabaseInfoTestDataBuilder
    {
        public const string HashPrefix = "Database Info Test Hash ";
        public const string NamePrefix = "Database Info Test Name ";
        private static long counter = 0;
        public string Hash { get; set; }
        public string Name { get; set; }
        public List<RackTestDataBuilder> Racks { get; set; } = new List<RackTestDataBuilder>();

        public static DatabaseInfoTestDataBuilder CreateDefault()
        {
            return new DatabaseInfoTestDataBuilder()
            .WithHash(HashPrefix + counter++)
            .Named(NamePrefix + counter++);
        }

        public DatabaseInfoTestDataBuilder WithHash(string hash)
        {
            Hash = hash;
            return this;
        }

        public DatabaseInfoTestDataBuilder Named(string name)
        {
            Name = name;
            return this;
        }

        public DatabaseInfoTestDataBuilder Add(RackTestDataBuilder rack)
        {
            Racks.Add(rack);
            return this;
        }

        public DatabaseInfoEntity Build()
        {
            DatabaseInfoEntity entity = new()
            {
                Hash = Hash,
                Name = Name,
            };
            entity.Racks = Racks.Select(rTDB => rTDB.Using(entity).Build()).ToList();
            return entity;
        }
    }
}
