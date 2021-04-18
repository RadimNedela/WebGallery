using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.DBMaintenanceInterfaces;

namespace WebGalery.Core.Maintenance
{
    public class DatabaseInfo : IDatabaseInfo
    {
        private readonly DatabaseInfoEntity _entity;
        private readonly string _currentRackHash;

        public string CurrentDatabaseInfoName => _entity.Name;

        private Rack _rack;
        public IRack ActiveRack => _rack ??= new Rack(_entity.Racks.First(r => r.Hash == _currentRackHash));

        public DatabaseInfo(DatabaseInfoEntity entity, string currentRackHash)
        {
            _entity = entity;
            _currentRackHash = currentRackHash;
        }
    }
}
