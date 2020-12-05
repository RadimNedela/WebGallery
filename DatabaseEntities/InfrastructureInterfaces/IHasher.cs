namespace Domain.Services.InfrastructureInterfaces
{
    public interface IHasher
    {
        string ComputeFileContentHash(string path);
        string ComputeStringHash(string theString);
        string ComputeRandomStringHash(string somePrefix);
    }
}