using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Contents.Factories
{
    internal class DirectoryBinderFactory : IDirectoryBinderFactory
    {
        private readonly IDirectoryReader directoryReader;
        private readonly DisplayableFactory displayableFactory;
        private readonly IHasher hasher;

        public DirectoryBinderFactory(IDirectoryReader directoryReader, DisplayableFactory displayableFactory, IHasher hasher)
        {
            this.directoryReader = directoryReader;
            this.displayableFactory = displayableFactory;
            this.hasher = hasher;
        }

        public DirectoryBinder LoadDirectory(string localPath)
        {
            DirectoryBinder retVal = new();
            retVal.Name = directoryReader.GetDirectoryName(localPath);
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
