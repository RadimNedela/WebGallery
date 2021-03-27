using System.Collections.Generic;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Maintenance.Domain
{
    public class DatabaseInfoHelper
    {
        private readonly IDirectoryMethods _directoryMethods;
        private readonly IHasher _hasher;

        public DatabaseInfoHelper(IDirectoryMethods directoryMethods, IHasher hasher)
        {
            _directoryMethods = directoryMethods;
            _hasher = hasher;
        }

        internal DatabaseInfoEntity BuildNewDatabase(string databaseName)
        {
            string infoHash = _hasher.ComputeRandomStringHash(databaseName);
            var dbInfo = new DatabaseInfoEntity
            {
                Hash = infoHash,
                Name = databaseName,
                Racks = new List<RackEntity>()
            };
            AddNewRack(dbInfo);
            return dbInfo;
        }

        internal void AddNewRack(DatabaseInfoEntity dbInfo)
        {
            string rackHash = _hasher.ComputeRandomStringHash(dbInfo.Hash + "Default");
            var rackEntity = new RackEntity
            {
                Hash = rackHash,
                Name = "Default",
                MountPoints = new List<MountPointEntity>()
            };
            AddNewMountPoint(rackEntity);
            dbInfo.Racks.Add(rackEntity);
        }

        internal void AddNewMountPoint(RackEntity rackEntity)
        {
            var mountPoint = new MountPointEntity
            {
                Path = _directoryMethods.GetCurrentDirectoryName()
            };
            rackEntity.MountPoints.Add(mountPoint);
        }
    }
}