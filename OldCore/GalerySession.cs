namespace WebGalery.Core
{
    public class GalerySession : IGalerySession, IGalerySessionInitializer
    {
        public string ActiveDatabaseHash { get; private set; }

        public string ActiveRackHash { get; private set; }

        public void SetCurrentInfo(string databaseHash, string rackHash)
        {
            ActiveDatabaseHash = databaseHash;
            ActiveRackHash = rackHash;
        }
    }
}
