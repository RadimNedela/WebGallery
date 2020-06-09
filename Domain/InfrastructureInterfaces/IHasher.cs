namespace Domain.InfrastructureInterfaces
{
    public interface IHasher
    {
        string GetImageHash(string path);
    }
}