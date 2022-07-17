using WebGalery.Domain.Contents;
using WebGalery.Domain.FileServices;
using WebGalery.Domain.Warehouses;

namespace WebGalery.Domain.Databases.Factories
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

        public Depot BuildDefaultFor(Depository parent)
        {
            var location = _locationFactory.CreateDefault();
            string name = "Rack " + location.Name + " " + DateTime.Now.ToString("G");
            string hash = _hasher.ComputeDependentStringHash(parent, name);

            ISet<Binder> childBinders = null;
            ISet<IDisplayable> displayables = null;
            var locations = new HashSet<ILocation> { location };

            var depot = new Depot(parent, hash, name, childBinders, displayables, locations);
            return depot;
        }
    }
}
