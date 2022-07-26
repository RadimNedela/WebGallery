namespace WebGalery.Core
{
    public interface IGalerySession
    {
        string ActiveDatabaseHash { get; }
        string ActiveRackHash { get; }
    }
}