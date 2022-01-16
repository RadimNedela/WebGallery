namespace WebGalery.Domain.Contents
{
    public class DirectoryBinder : Binder, IDirectoryBinder
    {
        public IHashedEntity Parent { get; }

        public DirectoryBinder(IHashedEntity parent)
        {
            Parent = parent;
        }
    }
}