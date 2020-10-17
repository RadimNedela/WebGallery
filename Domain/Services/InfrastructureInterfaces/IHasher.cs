using System.IO;

namespace Domain.InfrastructureInterfaces
{
    public interface IHasher
    {
        string ComputeFileContentHash(string path);
        string ComputeStringHash(string theString);
    }
}