using Domain.DbEntities.Maintenance;
using Domain.Dtos.Maintenance;
using Domain.Elements.Maintenance;
using Domain.InfrastructureInterfaces;
using System;

namespace Domain.Services
{
    public class DatabaseInfoBuilder
    {
        private IHasher hasher;
        private readonly IDatabaseInfoEntityRepository repository;
        private readonly IDirectoryMethods directoryMethods;

        public DatabaseInfoBuilder(
            IHasher hasher, 
            IDatabaseInfoEntityRepository repository, 
            IDirectoryMethods directoryMethods)
        {
            this.hasher = hasher;
            this.repository = repository;
            this.directoryMethods = directoryMethods;
        }

        public DatabaseInfoElement Create(DatabaseInfoEntity entity)
        {
            return new DatabaseInfoElement(hasher, entity);
        }

        public DatabaseInfoElement Create(DatabaseInfoDto dto)
        {
            var element = GetDatabase(dto.Hash);
            element.Merge(dto);
            return element;
        }

        public DatabaseInfoElement GetDatabase(string hash)
        {
            var entity = repository.Get(hash);
            var element = new DatabaseInfoElement(hasher, entity);
            return element;
        }

        public DatabaseInfoElement BuildNewDatabase(string databaseName)
        {
            var infoElement = new DatabaseInfoElement(hasher, databaseName, "Default", directoryMethods.GetCurrentDirectoryName());

            return infoElement;
        }
    }
}
