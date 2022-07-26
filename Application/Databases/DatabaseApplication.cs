using WebGalery.Database.Databases;
using WebGalery.Domain.Warehouses;
using WebGalery.Domain.Warehouses.Factories;

namespace Application.Databases
{
    public class DatabaseApplication
    {
        private readonly IGaleryDatabase _galeryDatabase;
        private readonly IDepositoryFactory _depositoryFactory;

        public DatabaseApplication(
            IGaleryDatabase galeryDatabase,
            IDepositoryFactory depositoryFactory)
        {
            _galeryDatabase = galeryDatabase;
            _depositoryFactory = depositoryFactory;
        }

        public DatabaseDto CreateNewDatabase(DatabaseDto databaseDto)
        {
            var domain = _depositoryFactory.Build(databaseDto.Name);
            _galeryDatabase.Depositories.Add(domain);
            _galeryDatabase.SaveChanges();
            return Convert(domain);
        }

        private DatabaseDto Convert(Depository domain)
        {
            var dto = new DatabaseDto
            {
                Name = domain.Name,
                Hash = domain.Hash,
            };
            return dto;
        }
    }
}
