using Domain.DbEntities.Maintenance;
using Domain.Elements.Maintenance;
using Domain.InfrastructureInterfaces;
using System;

namespace Domain.Services
{
    public class DatabaseInfoBuilder
    {
        private IHasher hasher;
        private readonly IDatabaseInfoEntityRepository repository;

        public DatabaseInfoBuilder(IHasher hasher, IDatabaseInfoEntityRepository repository)
        {
            this.hasher = hasher;
            this.repository = repository;
        }

        public DatabaseInfoElement Create(DatabaseInfoEntity entity)
        {
            return new DatabaseInfoElement(hasher, entity);
        }

        public DatabaseInfoElement GetDatabase(string hash)
        {
            var entity = repository.Get(hash);
            var element = new DatabaseInfoElement(hasher, entity);
            return element;
        }

        public DatabaseInfoElement BuildNewDatabase(string databaseName)
        {
            var infoElement = new DatabaseInfoElement(hasher, databaseName);

            return infoElement;
        }
    }
}
