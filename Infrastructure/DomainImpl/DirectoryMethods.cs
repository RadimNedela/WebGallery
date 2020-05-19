using System.Collections.Generic;
using System.IO;
using Domain.InfrastructureInterfaces;

namespace Infrastructure.DomainImpl
{
    public class DirectoryMethods : IDirectoryMethods
    {
        public IEnumerable<string> GetFileNames(string directoryName)
        {
            var files = Directory.GetFiles(directoryName);
            return null;
        }

    }
}
