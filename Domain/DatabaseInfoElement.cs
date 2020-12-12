using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DbEntities.Maintenance;
using Domain.Services.InfrastructureInterfaces;

namespace Domain.Elements.Maintenance
{
    public class DatabaseInfoElement
    {
        private readonly IHasher _hasher;

        public string Hash { get; }
        public string Name { get; private set; }
        public IList<RackElement> Racks { get; }

        public DatabaseInfoEntity Entity { get; }

        public DatabaseInfoElement(IHasher hasher, DatabaseInfoEntity databaseInfoEntity)
        {
            _hasher = hasher;
            Entity = databaseInfoEntity;
            Hash = Entity.Hash;
            Name = Entity.Name;
            Racks = Entity.Racks.Select(re => new RackElement(hasher, re)).ToList();
        }

        public DatabaseInfoElement(IHasher hasher, string databaseName, string initialRackName, string initialMountPoint)
        {
            _hasher = hasher;

            Name = databaseName;
            Hash = hasher.ComputeRandomStringHash(databaseName);
            Racks = new List<RackElement>();

            Entity = new DatabaseInfoEntity
            {
                Name = Name,
                Hash = Hash,
                Racks = new List<RackEntity>(),
            };

            AddNewRack(initialRackName, initialMountPoint);
        }

        public void Merge(DatabaseInfoDto dto)
        {
            if (dto.Name != Name)
            {
                Name = dto.Name;
                Entity.Name = Name;
            }
            foreach (var rackDto in dto.Racks)
            {
                if (rackDto.Hash != null)
                {
                    var existingRack = Racks.FirstOrDefault(r => r.Hash == rackDto.Hash);
                    if (existingRack == null)
                        throw new Exception($"Cannot find rack with hash {rackDto.Hash}. Database: {this}");
                    existingRack.Merge(rackDto);
                } else
                {
                    AddNewRack(rackDto.Name, rackDto.MountPoints);
                }
            }
        }

        public void AddNewRack(string name, string initialMountPointPath)
        {
            AddNewRack(name, new[] { initialMountPointPath });
        }

        public void AddNewRack(string name, IEnumerable<string> initialMountPoints)
        {
            if (Racks.Any(r => r.Name == name))
                throw new NotSupportedException("Cannot add new rack with already existing name, the name must be unique in the database");

            var rack = new RackElement(Entity, _hasher, name, initialMountPoints);
            Racks.Add(rack);
            Entity.Racks.Add(rack.Entity);
        }
    }
}
