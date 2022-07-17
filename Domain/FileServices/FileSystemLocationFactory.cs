using WebGalery.Domain.Warehouses;
using WebGalery.Domain.Warehouses.Factories;

namespace WebGalery.Domain.FileServices
{
    public class FileSystemLocationFactory : ILocationFactory
    {
        private readonly IDirectoryReader _directoryReader;

        public FileSystemLocationFactory(IDirectoryReader directoryReader)
        {
            _directoryReader = directoryReader;
        }

        public ILocation CreateDefault()
        {
            return new FileSystemLocation(_directoryReader);
        }
    }
}