using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Contents.Factories
{
    internal class DirectoryBinderFactory : IDirectoryBinderFactory
    {
        private readonly IDirectoryReader directoryReader;
        private readonly DisplayableFactory displayableFactory;
        private readonly IHasher hasher;
        private readonly ISessionProvider sessionProvider;

        public DirectoryBinderFactory(
            IDirectoryReader directoryReader, 
            DisplayableFactory displayableFactory, 
            IHasher hasher,
            ISessionProvider sessionProvider)
        {
            this.directoryReader = directoryReader;
            this.displayableFactory = displayableFactory;
            this.hasher = hasher;
            this.sessionProvider = sessionProvider;
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
                displayableFactory.AddDisplayable(currentBinder.Displayables, file);
            }
        }

        private Binder GetBinderFromPath(string localPath)
        {
            var rootPath = sessionProvider.Session.ActiveRootPath;
            var names = rootPath.SplitPath(localPath);
            Binder currentBinder = sessionProvider.Session.ActiveRack;
            foreach (var name in names)
            {
                var child = currentBinder.ChildBinders.FirstOrDefault(x => x.Name == name);
                if (child == null) child = new DirectoryBinder()
                        .Initialize(currentBinder, name, hasher.ComputeDependentStringHash(currentBinder, name));
                currentBinder = child;
            }

            return currentBinder;
        }
    }
}
