namespace WebGalery.Core.DBMaintenanceInterfaces
{
    public interface IRack
    {
        string Name { get; }
        string ActiveDirectory { get; }
        string GetSubpath(string fullPath);
    }
}