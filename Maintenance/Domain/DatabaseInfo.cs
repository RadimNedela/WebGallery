using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Maintenance.Domain
{
    public class DatabaseInfo
    {
        private DatabaseInfoEntity entity;
        private readonly string currentRackHash;

        public string CurrentDatabaseInfoName => entity.Name;

        private Rack _rack;
        public Rack CurrentRack => _rack ??= new Rack(entity.Racks.First(r => r.Hash == currentRackHash));

        public DatabaseInfo(DatabaseInfoEntity entity, string currentRackHash)
        {
            this.entity = entity;
            this.currentRackHash = currentRackHash;
        }
    }
}
