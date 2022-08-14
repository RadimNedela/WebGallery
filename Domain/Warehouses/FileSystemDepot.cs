using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Warehouses
{
    public class FileSystemDepot : Depot
    {
        private readonly ISet<FileSystemLocation> _locations;

        public IReadOnlySet<FileSystemLocation> FileSystemLocations => _locations.AsReadonlySet(nameof(Locations));

        protected override IEnumerable<ILocation> Locations => FileSystemLocations;

        public FileSystemDepot(
            Depository depository,
            string hash,
            string name,
            ISet<FileSystemLocation> locations,
            ISet<FileSystemRootRack> racks)
            : base(depository, hash, name, racks)
        {
            _locations = locations ?? new HashSet<FileSystemLocation>();
        }
    }
}
