using Domain.Dtos.Maintenance;
using Domain.Elements.Maintenance;
using Domain.InfrastructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class DatabaseInfoApplication
    {
        private readonly DatabaseInfoBuilder infoBuilder;
        private readonly IDatabaseInfoEntityRepository repository;
        public DatabaseInfoApplication(DatabaseInfoBuilder infoBuilder, IDatabaseInfoEntityRepository repository)
        {
            this.infoBuilder = infoBuilder;
            this.repository = repository;
        }

        public IEnumerable<DatabaseInfoDto> GetAllDatabases()
        {
            var allEntities = repository.GetAll();
            var allElements = allEntities.Select(e => infoBuilder.Create(e));
            var allDtos = allElements.Select(e => e.ToDto());

            return allDtos;
        }

        public DatabaseInfoDto CreateNewDatabase(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
                throw new Exception("Please give correct name to the new created database");
            var element = infoBuilder.BuildNewDatabase(databaseName);
            repository.Add(element.Entity);
            repository.Save();

            return element.ToDto();
        }

        public DatabaseInfoDto PersistDatabase(DatabaseInfoDto dto)
        {
            var element = infoBuilder.Create(dto);
            return element.ToDto();
        }

        public DatabaseInfoDto AddNewRack(string databaseHash, string name, string initialMountPointPath)
        {
            var element = infoBuilder.GetDatabase(databaseHash);
            element.AddNewRack(name, initialMountPointPath);
            repository.Save();

            return element.ToDto();
        }
    }
}
