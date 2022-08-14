using WebGalery.Domain.Basics;
using WebGalery.Domain.FileServices;
using WebGalery.Domain.SessionHandling;
using WebGalery.Domain.Warehouses.Factories;

namespace WebGalery.Domain.Warehouses.Loaders
{
    public class FileSystemDirectoryLoader
    {
        private readonly IDirectoryReader _directoryReader;
        private readonly IHasher _hasher;
        private readonly ISessionProvider _sessionProvider;
        private readonly IFileReader _fileReader;
        private readonly RackFactory _rackFactory;

        public FileSystemDirectoryLoader(
            IDirectoryReader directoryReader,
            IHasher hasher,
            ISessionProvider sessionProvider,
            IFileReader fileReader,
            RackFactory rackFactory)
        {
            _directoryReader = directoryReader;
            _hasher = hasher;
            _sessionProvider = sessionProvider;
            _fileReader = fileReader;
            _rackFactory = rackFactory;
        }

        public RackBase? LoadDirectory(string localPath)
        {
            RackBase? currentRack = GetRackFromPath(localPath);

            if (currentRack != null)
            {
                AddStorables(localPath, currentRack);
                RecurseIntoSubdirectories(localPath);
            }
            return currentRack;
        }

        private void RecurseIntoSubdirectories(string localPath)
        {
            foreach (var innerDirectory in _directoryReader.GetDirectories(localPath))
                LoadDirectory(innerDirectory);
        }

        private void AddStorables(string localPath, RackBase currentRack)
        {
            foreach (var file in _directoryReader.GetFileNames(localPath))
            {
                var fileName = _fileReader.GetFileName(file);
                var hash = _hasher.ComputeDependentStringHash(currentRack, fileName);
                var entityHash = _hasher.ComputeFileContentHash(file);

                currentRack.AddOrReplaceStorable(hash, fileName, entityHash);
            }
        }

        private RackBase? GetRackFromPath(string localPath)
        {
            var activeLocation = _sessionProvider.Session.ActiveLocation;
            if (activeLocation is not FileSystemLocation location)
                throw new ArgumentException($"Location is not {nameof(FileSystemLocation)}, but {activeLocation.GetType().FullName}", nameof(Session.ActiveLocation));
            var directoryNames = location.SplitJourneyToLegs(localPath);
            var depot = _sessionProvider.Session.ActiveDepot;
            IRacksHolder parentRacksHolder = depot;
            Entity parentEntity = depot;
            RackBase? rack = null;
            foreach (var directoryName in directoryNames)
            {
                rack = parentRacksHolder.Racks.FirstOrDefault(x => x.Name == directoryName);
                if (rack == null)
                {
                    rack = _rackFactory.BuildFileSystemRack(parentEntity, directoryName);
                    parentRacksHolder.AddRack(rack);
                }
                parentEntity = rack;
                parentRacksHolder = rack;
            }

            return rack;
        }
    }
}
