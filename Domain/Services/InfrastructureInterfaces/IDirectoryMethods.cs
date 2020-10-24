using System.Collections.Generic;
using System.IO;

namespace Domain.InfrastructureInterfaces
{
    public interface IDirectoryMethods
    {
        string GetCurrentDirectoryName();
        IEnumerable<string> GetFileNames(string directoryName);

        IEnumerable<string> GetDirectories(string directoryName);
    }
}