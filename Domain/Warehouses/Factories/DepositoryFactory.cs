using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses.Factories
{
    internal class DepositoryFactory
    {
        private readonly IHasher _hasher;
        private readonly FileSystemDepotFactory _fileSystemDepotFactory;

        public DepositoryFactory(IHasher hasher, FileSystemDepotFactory fileSystemDepotFactory)
        {
            _hasher = hasher;
            _fileSystemDepotFactory = fileSystemDepotFactory;
        }

        public Depository Build(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) name = null;
            name ??= "Database " + _hasher.CreateRandomString(5, 10) + DateTime.Now.ToString("F");
            string hash = _hasher.ComputeRandomStringHash(name);

            var db = new Depository(hash, name, null);
            db.AddDepot(_fileSystemDepotFactory.BuildDefaultFor(db));

            return db;
        }
    }
}
