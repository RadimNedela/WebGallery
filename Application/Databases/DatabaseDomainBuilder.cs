using WebGalery.Domain.Databases;
using WebGalery.Domain.Databases.Factories;

namespace Application.Databases
{
    public class DatabaseDomainBuilder : IDatabaseDomainBuilder
    {
        IDatabaseFactory _databaseFactory;

        public DatabaseDomainBuilder(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public Database BuildDomain(DatabaseDto databaseDto)
        {
            var database = _databaseFactory.Create();
            database.Name = databaseDto.Name;

            return database;
        }
    }
}
