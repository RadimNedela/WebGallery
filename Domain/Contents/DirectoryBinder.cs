using WebGalery.Domain.Databases;

namespace WebGalery.Domain.Contents
{
    public class DirectoryBinder : Binder, IDirectoryBinder
    {
        private Rack ParentRack { get; }

        public DirectoryBinder(Rack parentRack)
        {
            ParentRack = parentRack;
        }
    }
}