using WebGalery.Domain.Basics;
using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Warehouses.Factories
{
    internal class FileSystemDepotFactory
    {
        private readonly IHasher _hasher;
        private readonly FileSystemLocationFactory _locationFactory;

        public FileSystemDepotFactory(IHasher hasher, FileSystemLocationFactory locationFactory)
        {
            _hasher = hasher;
            _locationFactory = locationFactory;
        }

        public FileSystemDepot BuildDefaultFor(Depository depository)
        {
            string name = "FileSystemDepot " + _hasher.CreateRandomString(5, 10) + " " + DateTime.Now.ToString("G");
            string hash = _hasher.ComputeDependentStringHash(depository, name);

            var locations = new List<FileSystemLocation>();
            var depot = new FileSystemDepot(depository, hash, name, locations, null);

            var location = _locationFactory.CreateDefaultFor(depot);
            locations.Add(location);

            return depot;
        }
    }
}
