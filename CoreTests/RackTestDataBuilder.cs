using System.Collections.Generic;
using System.Linq;
using WebGalery.Core.Maintenance;

namespace WebGalery.Core.Tests
{
    public class RackTestDataBuilder
    {
        private static long _counter;
        public string Hash { get; private set; }
        public string Name { get; private set; }
        public DatabaseInfo Database { get; private set; }

        public static RackTestDataBuilder CreateDefault()
        {
            return new RackTestDataBuilder()
                .WithHash("Rack Test Hash " + _counter++)
                .WithName("Rack Test Name " + _counter++);
        }

        public string DatabaseHash { get; private set; }
        public List<MountPointTestDataBuilder> MountPoints { get; private set; } = new List<MountPointTestDataBuilder>();

        public RackTestDataBuilder WithHash(string hash)
        {
            Hash = hash;
            return this;
        }

        public RackTestDataBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        internal RackTestDataBuilder Using(DatabaseInfo database)
        {
            Database = database;
            DatabaseHash = database.Hash;
            return this;
        }

        public RackTestDataBuilder Add(MountPointTestDataBuilder mountPoint)
        {
            MountPoints.Add(mountPoint);
            return this;
        }

        public Rack Build()
        {
            Rack entity = new()
            {
                Hash = Hash,
                Name = Name,
                Database = Database,
                DatabaseHash = DatabaseHash
            };
            entity.MountPoints = MountPoints.Select(mpTdb => mpTdb.Using(entity).Build()).ToList();

            return entity;
        }
    }
}