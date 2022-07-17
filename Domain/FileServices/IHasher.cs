namespace WebGalery.Domain.FileServices
{
    public interface IHasher
    {
        string ComputeFileContentHash(string path);
        string ComputeStringHash(string theString);
        string ComputeDependentStringHash(IEntity parent, string theString);
        string ComputeRandomStringHash(string somePrefix);
        string CreateRandomString(int minLength, int maxLength);
    }
}