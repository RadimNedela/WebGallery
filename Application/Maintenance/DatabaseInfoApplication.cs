using Domain.Elements.Maintenance;
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

        public DatabaseInfoElement CreateNewDatabase(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
                throw new Exception("Please give correct name to the new created database");
            var element = infoBuilder.BuildNewDatabase(databaseName);
            repository.Add(element.Entity);
            repository.Save();

            return element;
        }

        public DatabaseInfoElement AddNewRack(string databaseHash, string name, string initialMountPointPath)
        {
            var element = infoBuilder.GetDatabase(databaseHash);
            element.AddNewRack(name, initialMountPointPath);
            repository.Save();

            return element;
        }
    }
}
