using WebGalery.Core;

namespace WebGalery.Application.SessionHandling
{
    public class DatabaseInfoProvider : IGalerySession, IDatabaseInfoInitializer
    {
        public string CurrentDatabaseHash { get; set; }

        public string CurrentRackHash { get; set; }

        public void SetCurrentInfo(string databaseHash, string rackHash)
        {
            CurrentDatabaseHash = databaseHash;
            CurrentRackHash = rackHash;
        }
    }
}
