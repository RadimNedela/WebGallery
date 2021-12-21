using System.Collections.Generic;

namespace WebGalery.Core.InfrastructureInterfaces
{
    public interface IDirectoryReader
    {
        IEnumerable<string> GetFileNames(string directoryName);

        IEnumerable<string> GetDirectories(string directoryName);
    }
}