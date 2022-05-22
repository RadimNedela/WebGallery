using WebGalery.Database.Databases;

namespace Application.Databases
{
    public class DatabaseApplication
    {
        private readonly IGaleryDatabase _galeryDatabase;
        private readonly IDatabaseDomainBuilder _databaseBuilder;

        public DatabaseApplication(IGaleryDatabase galeryDatabase, IDatabaseDomainBuilder databaseBuilder)
        {
            _galeryDatabase = galeryDatabase;
            _databaseBuilder = databaseBuilder;
        }

        public DatabaseDto CreateDatabase(DatabaseDto databaseDto)
        {
            var domain = _databaseBuilder.BuildDomain(databaseDto);
            _galeryDatabase.SaveChanges();
            return databaseDto;
        }
    }
}
