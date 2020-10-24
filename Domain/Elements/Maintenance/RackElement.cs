using Domain.DbEntities.Maintenance;
using Domain.Dtos.Maintenance;
using Domain.InfrastructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Elements.Maintenance
{
    public class RackElement
    {
        private readonly IHasher _hasher;
        public RackEntity Entity { get; }

        public string Hash { get; private set; }
        public string Name { get; private set; }
        public List<string> MountPoints { get; private set; }

        public RackElement(IHasher hasher, RackEntity entity)
        {
            _hasher = hasher;
            Entity = entity;
            Hash = entity.Hash;
            Name = entity.Name;
            MountPoints = entity.MountPoints.Select(mpe => mpe.Path).ToList();
        }

        public RackElement(DatabaseInfoEntity diEntity, IHasher hasher, string name, IEnumerable<string> initialMountPoints)
        {
            _hasher = hasher;
            Name = name;
            MountPoints = initialMountPoints.Select(s => s).ToList();

            string rackHash = hasher.ComputeStringHash($"{diEntity.Hash} {name}");

            var mountPointEntities = new List<MountPointEntity>();

            Entity = new RackEntity()
            {
                Database = diEntity,
                Hash = rackHash,
                Name = name,
                MountPoints = mountPointEntities
            };

            foreach (var mountPoint in MountPoints)
            {
                var mountPointEntity = new MountPointEntity
                {
                    Path = mountPoint,
                    Rack = Entity
                };
                mountPointEntities.Add(mountPointEntity);
            }
        }

        internal void Merge(RackDto rackDto)
        {
            if (Hash != rackDto.Hash)
                throw new Exception($"Tož to né - haš nesouhlasí {rackDto.Hash} != {this}");
            if (Name != rackDto.Name)
            {
                Name = rackDto.Name;
                Entity.Name = Name;
            }

            // nejprve smažeme...
            for (int i = MountPoints.Count - 1; i >= 0; i--)
            {
                if (rackDto.MountPoints.Contains(MountPoints[i]))
                    continue;
                // není v dto seznamu, smaž ho
                MountPoints.RemoveAt(i);
                Entity.MountPoints.RemoveAt(i);
            }

            foreach (var mountPointDto in rackDto.MountPoints)
            {
                if (MountPoints.Contains(mountPointDto))
                    continue;
                // není tam, vytvořit nový...
                MountPoints.Add(mountPointDto);
                Entity.MountPoints.Add(new MountPointEntity() { Path = mountPointDto, Rack = Entity });
            }
        }

        public string GetSubpath(string fullPath)
        {
            foreach (var mountPoint in MountPoints)
            {
                if (fullPath.StartsWith(mountPoint))
                {
                    return fullPath.Substring(mountPoint.Length);
                }
            }
            throw new System.Exception($"Sorry, the given path {fullPath} does not start with any known mount point");
        }
    }
}
