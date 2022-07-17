using WebGalery.Domain.Databases.Factories;
using WebGalery.Domain.Warehouses;

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