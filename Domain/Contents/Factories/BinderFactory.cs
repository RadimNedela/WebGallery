using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Domain.Contents.Factories
{
    internal class BinderFactory : IBinderFactory
    {
        private readonly IDirectoryReader directoryReader;
        private readonly DisplayableFactory displayableFactory;

        public BinderFactory(IDirectoryReader directoryReader, DisplayableFactory displayableFactory)
        {
            this.directoryReader = directoryReader;
            this.displayableFactory = displayableFactory;
        }

        public Binder LoadDirectory(string localPath)
        {
            Binder retVal = new();
            retVal.Name = directoryReader.GetDirectoryName(localPath);

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
