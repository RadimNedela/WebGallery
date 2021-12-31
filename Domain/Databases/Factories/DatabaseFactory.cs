using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Databases.Factories
{
    internal class DatabaseFactory
    {
        private readonly IHasher hasher;

        public DatabaseFactory(IHasher hasher)
        {
            this.hasher = hasher;
        }

        public Database Create()
        {
            var db = new Database()
            {
                Name = "Database " + hasher.CreateRandomString(5, 10) + DateTime.Now.ToString("F")
            };
            db.Hash = hasher.ComputeRandomStringHash(db.Name);
            return db;
        }
    }
}
