using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Contents.Factories
{
    internal class DirectoryBinderFactory : IDirectoryBinderFactory
    {
        private readonly IDirectoryReader directoryReader;
        private readonly IHasher hasher;
        private readonly ISessionProvider sessionProvider;
        private readonly IFileReader fileReader;

        public DirectoryBinderFactory(
            IDirectoryReader directoryReader,
            IHasher hasher,
            ISessionProvider sessionProvider,
            IFileReader fileReader)
        {
            this.directoryReader = directoryReader;
            this.hasher = hasher;
            this.sessionProvider = sessionProvider;
            this.fileReader = fileReader;
        }

        public Binder LoadDirectory(string localPath)
        {
            Binder currentBinder = GetBinderFromPath(localPath);

            AddDisplayables(localPath, currentBinder);
            RecurseIntoSubdirectories(localPath);

            return currentBinder;
        }

        private void RecurseIntoSubdirectories(string localPath)
        {
            foreach (var innerDirectory in directoryReader.GetDirectories(localPath))
            {
                LoadDirectory(innerDirectory);
            }
        }

        private void AddDisplayables(string localPath, Binder currentBinder)
        {
            foreach (var file in directoryReader.GetFileNames(localPath))
            {
                var fileName = fileReader.GetFileName(file);
                var hash = hasher.ComputeFileContentHash(file);

                currentBinder.AddOrReplaceDisplayable(fileName, hash);
            }
        }

        private Binder GetBinderFromPath(string localPath)
        {
            var activeLocation = sessionProvider.Session.ActiveLocation;
            if (activeLocation is not FileSystemLocation location)
                throw new ArgumentException($"Location is not {nameof(FileSystemLocation)}, but {activeLocation.GetType().FullName}", nameof(Session.ActiveLocation));
            var directories = location.SplitJourneyToLegs(localPath);
            Binder depot = sessionProvider.Session.ActiveDepot;
            foreach (var name in directories)
            {
                var child = depot.ChildBinders.FirstOrDefault(x => x.Name == name);
                if (child != null && child is not DirectoryBinder directoryBinder)
                    throw new Exception($"Trying to parse directories, but in the binders of {depot} I have found binder {child} that is not {nameof(DirectoryBinder)}");
                if (child == null)
                {
                    string hash = hasher.ComputeDependentStringHash(depot, name);
                    directoryBinder = new DirectoryBinder(hash, depot, name, null, null);
                    depot.AddChildBinder(directoryBinder);
                }
                depot = child;
            }

            return depot;
        }
    }
}
