using System.Collections.Generic;
using System.IO;
using Domain.InfrastructureInterfaces;

namespace Infrastructure.DomainImpl
{
    public class DirectoryMethods : IDirectoryMethods
    {
        public IEnumerable<string> GetFileNames(string directoryName)
        {
            if (string.IsNullOrWhiteSpace(directoryName)) directoryName = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(directoryName);
            return files;
        }

        public IEnumerable<string> GetDirectories(string directoryName)
        {
            if (string.IsNullOrWhiteSpace(directoryName)) directoryName = Directory.GetCurrentDirectory();
            var directories = Directory.GetDirectories(directoryName);
            return directories;
        }

        public Stream GetStream(string path)
        {
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous);
            return stream;
        }
    }
}
