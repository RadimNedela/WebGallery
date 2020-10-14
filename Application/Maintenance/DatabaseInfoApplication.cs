using Domain.InfrastructureInterfaces;
using System;

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

        public void CreateNewDatabase(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
                throw new Exception("Please give correct name to the new created database");
            var entity = infoBuilder.BuildNewDatabase(databaseName);
            repository.Add(entity);
            repository.Save();
        }
    }
}
