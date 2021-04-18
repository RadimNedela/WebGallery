namespace WebGalery.SessionHandling.Applications
{
    public interface IDatabaseInfoInitializer
    {
        void SetCurrentInfo(string databaseHash, string rackHash);
    }
}
