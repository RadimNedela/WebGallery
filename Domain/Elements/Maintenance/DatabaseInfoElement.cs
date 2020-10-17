using Domain.DbEntities.Maintenance;
using Domain.InfrastructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Elements.Maintenance
{
    public class DatabaseInfoElement
    {
        private readonly IHasher hasher;

        public string Hash { get; private set; }
        public string Name { get; private set; }
        public IList<string> Racks { get; private set; }

        public DatabaseInfoEntity Entity { get; }

        public DatabaseInfoElement(IHasher hasher, DatabaseInfoEntity databaseInfoEntity)
        {
            this.hasher = hasher;
            Entity = databaseInfoEntity;
            Hash = Entity.Hash;
            Racks = Entity.Racks.Select(re => re.Name).ToList();
        }

        public DatabaseInfoElement(IHasher hasher, string databaseName, string hash)
        {
            this.hasher = hasher;
            Name = databaseName;
            Hash = hash;
            Racks = new List<string>();

            Entity = new DatabaseInfoEntity
            {
                Name = Name,
                Hash = Hash,
                Racks = new List<RackEntity>(),
            };
        }

        public void AddNewRack(string name, string initialMountPointPath)
        {
            if (Racks.Contains(name))
                throw new NotSupportedException("Cannot add new rack with already existing name, the name must be unique in the database");
            Racks.Add(name);

            string rackHash = hasher.ComputeStringHash($"{Entity.Hash} {name}");
            string mountPointHash = hasher.ComputeStringHash($"{rackHash} {initialMountPointPath}");

            var mountPointEntity = new MountPointEntity
            {
                Hash = mountPointHash,
                Path = initialMountPointPath
            };

            var rackEntity = new RackEntity()
            {
                Database = Entity,
                Hash = rackHash,
                Name = name,
                MountPoints = new List<MountPointEntity> { mountPointEntity }
            };
            mountPointEntity.Rack = rackEntity;

            Entity.Racks.Add(rackEntity);
        }
    }
}
