using System;
using System.Collections.Generic;
using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.InfrastructureInterfaces.Base;
using WebGalery.Maintenance.Domain;

namespace WebGalery.Maintenance.Applications
{
    public class DatabaseInfoApplication
    {
        private readonly IDatabaseInfoEntityRepository repository;
        private readonly IEntityPersister<DatabaseInfoEntity> persister;
        private readonly DatabaseInfoHelper databaseInfoHandler;

        public DatabaseInfoApplication(
            IDatabaseInfoEntityRepository repository,
            IEntityPersister<DatabaseInfoEntity> persister,
            IDirectoryMethods directoryMethods,
            IHasher hasher)
        {
            this.repository = repository;
            this.persister = persister;
            databaseInfoHandler = new DatabaseInfoHelper(directoryMethods, hasher);
        }

        public IEnumerable<DatabaseInfoDto> GetAllDatabases()
        {
            DatabaseInfoDtoConverter converter = new DatabaseInfoDtoConverter();
            var allDatabases = repository.GetAll();
            var allDtos = allDatabases.Select(entity => converter.ToDto(entity));

            return allDtos;
        }

        public DatabaseInfoDto CreateNewDatabase(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
                throw new Exception("Please give correct name to the new created database");
            var newEntity = databaseInfoHandler.BuildNewDatabase(databaseName);
            persister.Add(newEntity);
            persister.Save();

            return new DatabaseInfoDtoConverter().ToDto(newEntity);
        }

        public DatabaseInfoDto UpdateDatabaseNames(DatabaseInfoDto dto)
        {
            var dbEntity = repository.Get(dto.Hash);
            var converter = new DatabaseInfoDtoConverter();
            converter.Merge(dbEntity, dto);

            persister.Save();

            return converter.ToDto(dbEntity);
        }

        public DatabaseInfoDto AddNewRack(DatabaseInfoDto dto)
        {
            var dbEntity = repository.Get(dto.Hash);
            databaseInfoHandler.AddNewRack(dbEntity);
            persister.Save();
            return new DatabaseInfoDtoConverter().ToDto(dbEntity);
        }

        public DatabaseInfoDto AddNewMountPoint(string databaseHash, string rackHash)
        {
            var infoEntity = repository.Get(databaseHash);
            var rack = infoEntity.Racks.First(r => r.Hash == rackHash);
            databaseInfoHandler.AddNewMountPoint(rack);

            persister.Save();

            return new DatabaseInfoDtoConverter().ToDto(infoEntity);
        }
    }
}
