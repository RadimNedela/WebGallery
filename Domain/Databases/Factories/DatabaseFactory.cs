using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Databases.Factories
{
    internal class DatabaseFactory : IDatabaseFactory
    {
        private readonly IHasher hasher;
        private readonly IRackFactory rackFactory;

        public DatabaseFactory(IHasher hasher, IRackFactory rackFactory)
        {
            this.hasher = hasher;
            this.rackFactory = rackFactory;
        }

        public Database Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) name = null;

            var db = new Database()
            {
                Name = name ?? "Database " + hasher.CreateRandomString(5, 10) + DateTime.Now.ToString("F")
            };
            db.Hash = hasher.ComputeRandomStringHash(db.Name);
            db.Racks.Add(rackFactory.CreateDefaultFor(db));
            return db;
        }
    }
}
