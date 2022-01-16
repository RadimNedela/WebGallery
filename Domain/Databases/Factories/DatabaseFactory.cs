using WebGalery.Domain.FileServices;
using WebGalery.Domain.IoC;

namespace WebGalery.Domain.Databases.Factories
{
    internal class DatabaseFactory
    {
        private readonly IHasher hasher;
        private readonly IRackFactory rackFactory;

        public DatabaseFactory(IHasher? hasher = null, IRackFactory? rackFactory = null)
        {
            this.hasher = hasher ?? IoCDefaults.Hasher;
            this.rackFactory = rackFactory ?? IoCDefaults.RackFactory;
        }

        public Database Create()
        {
            var db = new Database()
            {
                Name = "Database " + hasher.CreateRandomString(5, 10) + DateTime.Now.ToString("F")
            };
            db.Hash = hasher.ComputeRandomStringHash(db.Name);
            db.Racks.Add(rackFactory.CreateFor(db));
            return db;
        }
    }
}
