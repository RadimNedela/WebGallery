using WebGalery.Domain.Basics;
using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Warehouses.Factories
{
    internal class FileSystemDepotFactory : IDepotFactory
    {
        private readonly IHasher _hasher;
        private readonly FileSystemLocationFactory _locationFactory;

        public FileSystemDepotFactory(IHasher hasher, FileSystemLocationFactory locationFactory)
        {
            _hasher = hasher;
            _locationFactory = locationFactory;
        }

        public Depot BuildDefaultFor(Depository depository)
        {
            var location = _locationFactory.CreateDefault();
            string name = "RackBase " + location.Name + " " + DateTime.Now.ToString("G");
            string hash = _hasher.ComputeDependentStringHash(depository, name);

            var locations = new HashSet<FileSystemLocation> { location };

            var depot = new FileSystemDepot(depository, hash, name, locations, null);
            return depot;
        }
    }
}
