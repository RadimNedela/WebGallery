using System.Collections.Generic;

namespace Domain.InfrastructureInterfaces
{
    public interface IDirectoryMethods
    {
        IEnumerable<string> GetFileNames(string directoryName);
    }
}