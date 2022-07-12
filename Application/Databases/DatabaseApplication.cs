using WebGalery.Database.Databases;
using WebGalery.Domain.Databases;
using WebGalery.Domain.Databases.Factories;
using WebGalery.Domain.DBModel.Factories;

namespace Application.Databases
{
    public class DatabaseApplication
    {
        private readonly IGaleryDatabase _galeryDatabase;
        private readonly IDatabaseFactory _databaseFactory;
        private readonly IDatabaseInfoDBFactory _databaseInfoDBFactory;

        public DatabaseApplication(
            IGaleryDatabase galeryDatabase,
            IDatabaseFactory databaseFactory,
            IDatabaseInfoDBFactory databaseInfoDBFactory)
        {
            _galeryDatabase = galeryDatabase;
            _databaseFactory = databaseFactory;
            _databaseInfoDBFactory = databaseInfoDBFactory;
        }

        public DatabaseDto CreateNewDatabase(DatabaseDto databaseDto)
        {
            var domain = _databaseFactory.Create(databaseDto.Name);
            _galeryDatabase.DatabaseInfos.Add(_databaseInfoDBFactory.Build(domain));
            _galeryDatabase.SaveChanges();
            return Convert(domain);
        }

        //public IEnumerable<DatabaseDto> GetAllDatabases()
        //{
        //    _galeryDatabase
        //}

        private DatabaseDto Convert(Database domain)
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
