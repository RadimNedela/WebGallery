using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Core.DomainInterfaces
{
    public interface IDatabaseInfo
    {
        string CurrentDatabaseInfoName { get; }
        string CurrentRackName { get; }
        string ActiveDirectory { get; }
    }
}
