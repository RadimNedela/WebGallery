using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.DBMaintenanceInterfaces;

namespace WebGalery.Maintenance.Domain
{
    public class DatabaseInfo : IDatabaseInfo
    {
        private readonly DatabaseInfoEntity entity;
        private readonly string currentRackHash;

        public string CurrentDatabaseInfoName => entity.Name;

        private Rack _rack;
        public IRack ActiveRack => _rack ??= new Rack(entity.Racks.First(r => r.Hash == currentRackHash));

        public DatabaseInfo(DatabaseInfoEntity entity, string currentRackHash)
        {
            this.entity = entity;
            this.currentRackHash = currentRackHash;
        }
    }
}
