namespace Domain.InfrastructureInterfaces
{
    public interface IHasher
    {
        bool CanHandlePath(string path);
        string GetImageHash(string path);
    }
}