using WebGalery.Core.Maintenance;

namespace WebGalery.Core.DBMaintenanceInterfaces
{
    public interface IActiveRackService
    {
        string ActiveDirectory { get; }
        string GetSubpath(string fullPath);
        string ActiveDatabaseName { get; }
        string ActiveRackHash { get; }
        string ActiveRackName { get; }
    }
}