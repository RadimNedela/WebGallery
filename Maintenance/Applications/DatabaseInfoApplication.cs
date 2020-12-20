using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DbEntities.Maintenance;
using Domain.Dtos.Maintenance;
using Domain.Services.InfrastructureInterfaces;
using WebGalery.Maintenance.Services;

namespace Application.Maintenance
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
            string rackHash = _hasher.ComputeRandomStringHash(infoHash + " Default");
            var dbInfo = new DatabaseInfoEntity()
            {
                Hash = infoHash,
                Name = databaseName,
                Racks = new List<RackEntity> { new RackEntity
                {
                    Hash = rackHash,
                    Name = "Default",
                    MountPoints = new List<MountPointEntity>
                    {
                        new MountPointEntity
                        {
                            Path = _directoryMethods.GetCurrentDirectoryName()
                        }
                    }
                } }
            };
            return dbInfo;
        }


        //public DatabaseInfoDto PersistDatabase(DatabaseInfoDto dto)
        //{
        //    var element = _infoBuilder.Create(dto);

        //    _repository.Save();

        //    return element.ToDto();
        //}

        //public DatabaseInfoDto AddNewRack(DatabaseInfoDto dto)
        //{
        //    return AddNewRack(dto.Hash, "Default", _directoryMethods.GetCurrentDirectoryName());
        //}

        //public DatabaseInfoDto AddNewRack(string databaseHash, string name, string initialMountPointPath)
        //{
        //    var element = _infoBuilder.GetDatabase(databaseHash);
        //    element.AddNewRack(name, initialMountPointPath);
        //    _repository.Save();

        //    return element.ToDto();
        //}

        //public object AddNewMountPoint(string databaseHash, string rackHash)
        //{
        //    var database = _infoBuilder.GetDatabase(databaseHash);
        //    var rack = database.Racks.First(r => r.Hash == rackHash);
        //    rack.AddMountPoint(_directoryMethods.GetCurrentDirectoryName());

        //    _repository.Save();

        //    return database.ToDto();
        //}
    }
}
