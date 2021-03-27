namespace WebGalery.Core.DBMaintenanceInterfaces
{
    public interface IRack
    {
        string Hash { get; }
        string Name { get; }
        string ActiveDirectory { get; }
        string GetSubpath(string fullPath);
    }
}