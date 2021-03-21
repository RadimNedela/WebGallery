using System.Collections.Generic;
using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Core.Tests
{
    public class DatabaseInfoTestDataBuilder
    {
        public const string HashPrefix = "Database Info Test Hash ";
        public const string NamePrefix = "Database Info Test Name ";
        private static long _counter;
        public string Hash { get; set; }
        public string Name { get; set; }
        public List<RackTestDataBuilder> Racks { get; set; } = new List<RackTestDataBuilder>();

        public static DatabaseInfoTestDataBuilder CreateDefault()
        {
            return new DatabaseInfoTestDataBuilder()
            .WithHash(HashPrefix + _counter++)
            .Named(NamePrefix + _counter++);
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
            entity.Racks = Racks.Select(rTdb => rTdb.Using(entity).Build()).ToList();
            return entity;
        }
    }
}
