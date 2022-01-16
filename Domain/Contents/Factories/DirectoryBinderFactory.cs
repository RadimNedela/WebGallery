using WebGalery.Domain.FileServices;
using WebGalery.Domain.IoC;

namespace WebGalery.Domain.Contents.Factories
{
    internal class DirectoryBinderFactory : IDirectoryBinderFactory
    {
        private readonly IDirectoryReader directoryReader;
        private readonly IDisplayableFactory displayableFactory;
        private readonly IHasher hasher;
        private readonly ISessionProvider sessionProvider;

        public DirectoryBinderFactory(
            IDirectoryReader? directoryReader = null, 
            IDisplayableFactory? displayableFactory = null, 
            IHasher? hasher = null,
            ISessionProvider? sessionProvider = null)
        {
            this.directoryReader = directoryReader ?? IoCDefaults.DirectoryReader;
            this.displayableFactory = displayableFactory ?? IoCDefaults.DisplayableFactory;
            this.hasher = hasher ?? IoCDefaults.Hasher;
            this.sessionProvider = sessionProvider ?? IoCDefaults.SessionProvider;
        }

        public DirectoryBinder LoadDirectory(string localPath)
        {
            DirectoryBinder retVal = new(sessionProvider.Session.ActiveRack);
            var names = sessionProvider.Session.ActiveRootPath.NormalizePath(localPath);
            retVal.Name = names.First();
            retVal.Hash = hasher.ComputeStringHash(localPath);

            foreach (var innerDirectory in directoryReader.GetDirectories(localPath))
            {
                retVal.ChildBinders.Add(LoadDirectory(innerDirectory));
            }

            foreach (var file in directoryReader.GetFileNames(localPath))
            {
                var displayable = displayableFactory.CreateFromFile(file);
                if (displayable != null)
                    retVal.Displayables.Add(displayable);
            }

            return retVal;
        }
    }
}
