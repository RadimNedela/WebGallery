using Domain.DbEntities.Maintenance;

namespace Domain.Services
{
    public interface IDatabaseInfoProvider
    {
        DatabaseInfoEntity CurrentDatabaseInfo { get; }
        RackEntity CurrentRack { get; }
    }
}
