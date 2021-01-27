namespace WebGalery.Core.DBMaintenanceInterfaces
{
    public interface IDatabaseInfo
    {
        string CurrentDatabaseInfoName { get; }
        IRack ActiveRack { get; }
    }
}