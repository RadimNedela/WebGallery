using System.Collections.Generic;
using System.IO;
using Domain.InfrastructureInterfaces;

namespace Infrastructure.DomainImpl
{
    public class DirectoryMethods : IDirectoryMethods
    {
        public string GetCurrentDirectoryName()
        {
            return Directory.GetCurrentDirectory();
        }

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
    }
}
