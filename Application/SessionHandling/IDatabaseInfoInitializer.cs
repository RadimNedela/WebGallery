namespace WebGalery.Application.SessionHandling
{
    public interface IDatabaseInfoInitializer
    {
        void SetCurrentInfo(string databaseHash, string rackHash);
    }
}
