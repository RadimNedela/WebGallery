using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses.Factories
{
    internal class DepositoryFactory : IDepositoryFactory
    {
        private readonly IHasher _hasher;
        private readonly IDepotFactory _depotFactory;

        public DepositoryFactory(IHasher hasher, IDepotFactory rackFactory)
        {
            _hasher = hasher;
            _depotFactory = rackFactory;
        }

        public Depository Build(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) name = null;
            name ??= "Database " + _hasher.CreateRandomString(5, 10) + DateTime.Now.ToString("F");
            string hash = _hasher.ComputeRandomStringHash(name);

            var db = new Depository(hash, name, null);
            db.AddDepot(_depotFactory.BuildDefaultFor(db));

            return db;
        }
    }
}
