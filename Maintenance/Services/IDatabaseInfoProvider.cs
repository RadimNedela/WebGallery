using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Maintenance.Services
{
    public interface IDatabaseInfoProvider
    {
        DatabaseInfoEntity CurrentDatabaseInfo { get; }
        RackEntity CurrentRack { get; }
    }
}
