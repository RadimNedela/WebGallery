namespace WebGalery.Core
{
    public interface IGalerySession
    {
        string CurrentDatabaseHash { get; }
        string CurrentRackHash { get; }
    }
}