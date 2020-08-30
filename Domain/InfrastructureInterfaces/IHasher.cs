using System.IO;

namespace Domain.InfrastructureInterfaces
{
    public interface IHasher
    {
        string ComputeFileContentHash(Stream stream, string path);
        string ComputeDirectoryHash(string directoryPath);
    }
}