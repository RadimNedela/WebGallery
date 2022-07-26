using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses.Factories
{
    internal class DepotFactory : IDepotFactory
    {
        private readonly IHasher _hasher;
        private readonly ILocationFactory _locationFactory;

        public DepotFactory(IHasher hasher, ILocationFactory locationFactory)
        {
            _hasher = hasher;
            _locationFactory = locationFactory;
        }

        public Depot BuildDefaultFor(Depository depository)
        {
            var location = _locationFactory.CreateDefault();
            string name = "Rack " + location.Name + " " + DateTime.Now.ToString("G");
            string hash = _hasher.ComputeDependentStringHash(depository, name);

            var locations = new HashSet<ILocation> { location };

            var depot = new Depot(depository, hash, name, locations, null);
            return depot;
        }
    }
}
