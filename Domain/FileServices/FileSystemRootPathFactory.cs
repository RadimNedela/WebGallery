using WebGalery.Domain.Databases;
using WebGalery.Domain.Databases.Factories;

namespace WebGalery.Domain.FileServices
{
    internal class FileSystemRootPathFactory : IRootPathFactory
    {
        public IRootPath Create()
        {
            return new FileSystemRootPath(new DirectoryMethods());
        }
    }
}