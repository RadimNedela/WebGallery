using System;
using System.Collections.Generic;
using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Maintenance.Applications.InternalServices;

namespace WebGalery.Maintenance.Applications
{
    public class DatabaseInfoApplication
    {
        private readonly IDatabaseInfoEntityRepository _repository;
        private readonly IDirectoryMethods _directoryMethods;
        private readonly IHasher _hasher;

        public DatabaseInfoApplication(
            IDatabaseInfoEntityRepository repository,
            IDirectoryMethods directoryMethods,
            IHasher hasher)
        {
            _repository = repository;
            _directoryMethods = directoryMethods;
            _hasher = hasher;
        }

        public IEnumerable<DatabaseInfoDto> GetAllDatabases()
        {
            DatabaseInfoDtoConverter converter = new DatabaseInfoDtoConverter();
            var allDatabases = _repository.GetAll();
            var allDtos = allDatabases.Select(entity => converter.ToDto(entity));

            return allDtos;
        }

        public DatabaseInfoDto CreateNewDatabase(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
                throw new Exception("Please give correct name to the new created database");
            var newEntity = BuildNewDatabase(databaseName);
            _repository.Add(newEntity);
            _repository.Save();

            return new DatabaseInfoDtoConverter().ToDto(newEntity);
        }

        private DatabaseInfoEntity BuildNewDatabase(string databaseName)
        {
            string infoHash = _hasher.ComputeRandomStringHash(databaseName);
            var dbInfo = new DatabaseInfoEntity()
            {
                Hash = infoHash,
                Name = databaseName,
                Racks = new List<RackEntity>()
            };
            AddNewRack(dbInfo);
            return dbInfo;
        }

        public DatabaseInfoDto UpdateDatabaseNames(DatabaseInfoDto dto)
        {
            var dbEntity = _repository.Get(dto.Hash);
            var converter = new DatabaseInfoDtoConverter();
            converter.Merge(dbEntity, dto);

            _repository.Save();

            return converter.ToDto(dbEntity);
        }

        public DatabaseInfoDto AddNewRack(DatabaseInfoDto dto)
        {
            var dbEntity = _repository.Get(dto.Hash);
            AddNewRack(dbEntity);
            _repository.Save();
            return new DatabaseInfoDtoConverter().ToDto(dbEntity);
        }

        private void AddNewRack(DatabaseInfoEntity dbInfo)
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

        public DatabaseInfoDto AddNewMountPoint(string databaseHash, string rackHash)
        {
            var infoEntity = _repository.Get(databaseHash);
            var rack = infoEntity.Racks.First(r => r.Hash == rackHash);
            AddNewMountPoint(rack);

            _repository.Save();

            return new DatabaseInfoDtoConverter().ToDto(infoEntity);
        }

        private void AddNewMountPoint(RackEntity rackEntity)
        {
            var mountPoint = new MountPointEntity
            {
                Path = _directoryMethods.GetCurrentDirectoryName()
            };
            rackEntity.MountPoints.Add(mountPoint);
        }
    }
}
