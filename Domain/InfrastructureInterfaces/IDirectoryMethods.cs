using System.Collections.Generic;
using System.IO;

namespace Domain.InfrastructureInterfaces
{
    public interface IDirectoryMethods
    {
        IEnumerable<string> GetFileNames(string directoryName);

        IEnumerable<string> GetDirectories(string directoryName);
    }
}