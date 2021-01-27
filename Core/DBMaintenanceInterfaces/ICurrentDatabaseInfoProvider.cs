namespace WebGalery.Core.DBMaintenanceInterfaces
{
    public interface ICurrentDatabaseInfoProvider
    {
        IDatabaseInfo CurrentInfo { get; }
    }
}