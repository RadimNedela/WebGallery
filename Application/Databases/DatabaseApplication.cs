using WebGalery.Database.Databases;

namespace Application.Databases
{
    public class DatabaseApplication
    {
        private readonly IGaleryDatabase _galeryDatabase;

        public DatabaseApplication(IGaleryDatabase galeryDatabase)
        {
            _galeryDatabase = galeryDatabase;
        }

        public DatabaseDto CreateDatabase(DatabaseDto databaseDto)
        {
            throw new NotImplementedException();
        }
    }
}
