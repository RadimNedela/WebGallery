using System;
using System.Collections.Generic;
using System.Linq;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Maintenance.Domain;

namespace WebGalery.Maintenance.Applications
{
    public class DatabaseInfoApplication
    {
        private readonly IDatabaseInfoEntityRepository _repository;
        private readonly DatabaseInfoHelper _databaseInfoHandler;

        public DatabaseInfoApplication(
            IDatabaseInfoEntityRepository repository,
            IDirectoryMethods directoryMethods,
            IHasher hasher)
        {
            _repository = repository;
            _databaseInfoHandler = new DatabaseInfoHelper(directoryMethods, hasher);
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
            var newEntity = _databaseInfoHandler.BuildNewDatabase(databaseName);
            _repository.Add(newEntity);
            _repository.Save();

            return new DatabaseInfoDtoConverter().ToDto(newEntity);
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
            _databaseInfoHandler.AddNewRack(dbEntity);
            _repository.Save();
            return new DatabaseInfoDtoConverter().ToDto(dbEntity);
        }

        public DatabaseInfoDto AddNewMountPoint(string databaseHash, string rackHash)
        {
            var infoEntity = _repository.Get(databaseHash);
            var rack = infoEntity.Racks.First(r => r.Hash == rackHash);
            _databaseInfoHandler.AddNewMountPoint(rack);

            _repository.Save();

            return new DatabaseInfoDtoConverter().ToDto(infoEntity);
        }
    }
}
