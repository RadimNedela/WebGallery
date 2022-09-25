using WebGalery.Domain.Basics;
using WebGalery.Domain.Warehouses;

namespace WebGalery.Domain.FileServices
{
    public class FileSystemLocationFactory
    {
        private readonly IDirectoryReader _directoryReader;
        private readonly IHasher _hasher;

        public FileSystemLocationFactory(IDirectoryReader directoryReader, IHasher hasher)
        {
            _directoryReader = directoryReader;
            _hasher = hasher;
        }

        public FileSystemLocation CreateDefaultFor(FileSystemDepot parentDepot)
        {
            string name = FileSystemLocation.NormalizePath(_directoryReader.GetCurrentDirectoryName());
            string hash = _hasher.ComputeDependentStringHash(parentDepot, name);
            return new FileSystemLocation(parentDepot, hash, name);
        }
    }
}