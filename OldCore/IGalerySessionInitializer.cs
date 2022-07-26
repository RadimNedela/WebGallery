namespace WebGalery.Core
{
    public interface IGalerySessionInitializer
    {
        void SetCurrentInfo(string databaseHash, string rackHash);
    }
}
