using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Core.DomainInterfaces
{
    public interface IDatabaseInfo
    {
        DatabaseInfoEntity GetCurrentDatabaseInfo();
        RackEntity GetCurrentRack();
        string GetActiveDirectory();
    }
}
