using WebGalery.Domain.Databases;
using WebGalery.Domain.Databases.Factories;

namespace WebGalery.Domain.FileServices
{
    internal class FileSystemRootPathFactory : IRootPathFactory
    {
        private readonly IDirectoryReader _directoryReader;

        public FileSystemRootPathFactory(IDirectoryReader directoryReader)
        {
            _directoryReader = directoryReader;
        }
        public IRootPath Create()
        {
            return new FileSystemRootPath(_directoryReader);
        }
    }
}