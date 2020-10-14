using System.IO;

namespace Domain.InfrastructureInterfaces
{
    public interface IHasher
    {
        string ComputeFileContentHash(string path);
        string ComputeDirectoryHash(string directoryPath);
        string ComputeStringHash(string theString);
    }
}