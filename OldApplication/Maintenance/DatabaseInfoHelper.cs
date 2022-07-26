using System.Collections.Generic;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Maintenance;

namespace WebGalery.Application.Maintenance
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

        internal DatabaseInfo BuildNewDatabase(string databaseName)
        {
            string infoHash = _hasher.ComputeRandomStringHash(databaseName);
            var dbInfo = new DatabaseInfo
            {
                Hash = infoHash,
                Name = databaseName,
                Racks = new List<Rack>()
            };
            AddNewRack(dbInfo);
            return dbInfo;
        }

        internal void AddNewRack(DatabaseInfo dbInfo)
        {
            string rackHash = _hasher.ComputeRandomStringHash(dbInfo.Hash + "Default");
            var rackEntity = new Rack
            {
                Hash = rackHash,
                Name = "Default",
                MountPoints = new List<MountPoint>()
            };
            AddNewMountPoint(rackEntity);
            dbInfo.Racks.Add(rackEntity);
        }

        internal void AddNewMountPoint(Rack rackEntity)
        {
            var mountPoint = new MountPoint
            {
                Path = _directoryMethods.GetCurrentDirectoryName()
            };
            rackEntity.MountPoints.Add(mountPoint);
        }
    }
}