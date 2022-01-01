using WebGalery.Domain.Databases;

namespace WebGalery.Domain.FileServices
{
    internal class FileSystemRootPath : IRootPath
    {
        public string RootPath { get; private set; }

        public FileSystemRootPath(IDirectoryReader directoryReader)
        {
            RootPath = directoryReader.GetCurrentDirectoryName();
        }
    }
}
