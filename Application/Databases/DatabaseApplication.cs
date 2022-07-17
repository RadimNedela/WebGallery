using WebGalery.Database.Databases;
using WebGalery.Domain.DBModel.Factories;
using WebGalery.Domain.Warehouses;
using WebGalery.Domain.Warehouses.Factories;

namespace Application.Databases
{
    public class DatabaseApplication
    {
        private readonly IGaleryDatabase _galeryDatabase;
        private readonly IDepositoryFactory _databaseFactory;
        private readonly IDatabaseInfoDBFactory _databaseInfoDBFactory;

        public DatabaseApplication(
            IGaleryDatabase galeryDatabase,
            IDepositoryFactory databaseFactory,
            IDatabaseInfoDBFactory databaseInfoDBFactory)
        {
            _galeryDatabase = galeryDatabase;
            _databaseFactory = databaseFactory;
            _databaseInfoDBFactory = databaseInfoDBFactory;
        }

        public DatabaseDto CreateNewDatabase(DatabaseDto databaseDto)
        {
            var domain = _databaseFactory.Build(databaseDto.Name);
            _galeryDatabase.DatabaseInfos.Add(_databaseInfoDBFactory.Build(domain));
            _galeryDatabase.SaveChanges();
            return Convert(domain);
        }

        //public IEnumerable<DatabaseDto> GetAllDatabases()
        //{
        //    _galeryDatabase
        //}

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
