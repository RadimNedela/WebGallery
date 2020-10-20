using Domain.DbEntities.Maintenance;
using Domain.InfrastructureInterfaces;
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

        public RackElement(DatabaseInfoEntity diEntity, IHasher hasher, string name, string initialMountPointPath)
        {
            _hasher = hasher;
            Name = name;
            MountPoints = new List<string> { initialMountPointPath };

            string rackHash = hasher.ComputeStringHash($"{diEntity.Hash} {name}");
            string mountPointHash = hasher.ComputeStringHash($"{rackHash} {initialMountPointPath}");

            var mountPointEntity = new MountPointEntity
            {
                Hash = mountPointHash,
                Path = initialMountPointPath
            };

            Entity = new RackEntity()
            {
                Database = diEntity,
                Hash = rackHash,
                Name = name,
                MountPoints = new List<MountPointEntity> { mountPointEntity }
            };
            mountPointEntity.Rack = Entity;


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
