namespace WebGalery.Domain.Basics
{
    public interface IHasher
    {
        string ComputeFileContentHash(string path);
        string ComputeStringHash(string theString);
        string ComputeDependentStringHash(Entity parent, string theString);
        string ComputeRandomStringHash(string somePrefix);
        string CreateRandomString(int minLength, int maxLength);
    }
}