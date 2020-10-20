using Domain.Elements.Maintenance;

namespace Domain.Services
{
    public interface IDatabaseInfoProvider
    {
        DatabaseInfoElement CurrentDatabaseInfo { get; }
        RackElement CurrentRack { get; }
    }
}
