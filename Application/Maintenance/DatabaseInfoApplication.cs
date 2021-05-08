using System;
using System.Collections.Generic;
using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.InfrastructureInterfaces.Base;

namespace WebGalery.Application.Maintenance
{
    public class DatabaseInfoApplication
    {
        private readonly IDatabaseInfoRepository _repository;
        private readonly IPersister<DatabaseInfo> _persister;
        private readonly DatabaseInfoHelper _databaseInfoHandler;

        public DatabaseInfoApplication(
            IDatabaseInfoRepository repository,
            IPersister<DatabaseInfo> persister,
            IDirectoryMethods directoryMethods,
            IHasher hasher)
        {
            _repository = repository;
            _persister = persister;
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
            _persister.Add(newEntity);
            _persister.Save();

            return new DatabaseInfoDtoConverter().ToDto(newEntity);
        }

        public DatabaseInfoDto UpdateDatabaseNames(DatabaseInfoDto dto)
        {
            var dbEntity = _repository.Get(dto.Hash);
            var converter = new DatabaseInfoDtoConverter();
            converter.Merge(dbEntity, dto);

            _persister.Save();

            return converter.ToDto(dbEntity);
        }

        public DatabaseInfoDto AddNewRack(DatabaseInfoDto dto)
        {
            var dbEntity = _repository.Get(dto.Hash);
            _databaseInfoHandler.AddNewRack(dbEntity);
            _persister.Save();
            return new DatabaseInfoDtoConverter().ToDto(dbEntity);
        }

        public DatabaseInfoDto AddNewMountPoint(string databaseHash, string rackHash)
        {
            var infoEntity = _repository.Get(databaseHash);
            var rack = infoEntity.Racks.First(r => r.Hash == rackHash);
            _databaseInfoHandler.AddNewMountPoint(rack);

            _persister.Save();

            return new DatabaseInfoDtoConverter().ToDto(infoEntity);
        }

        public void DeleteDatabase(string databaseHash)
        {
            var infoEntity = _repository.Get(databaseHash);
            _persister.Remove(infoEntity);
            _persister.Save();
        }
    }
}
