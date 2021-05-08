using System.Collections.Generic;

namespace WebGalery.Core.InfrastructureInterfaces
{
    public interface IDirectoryMethods
    {
        string GetCurrentDirectoryName();

        IEnumerable<string> GetFileNames(string directoryName);

        IEnumerable<string> GetDirectories(string directoryName);

        bool Exists(string path);

        string NormalizePath(string path);
    }
}