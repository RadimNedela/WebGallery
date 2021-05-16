using System.Linq;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.Maintenance
{
    class ActiveRackService : IActiveRackService
    {
        private readonly IGalerySession _session;
        private readonly IDatabaseInfoRepository _databaseInfoRepository;
        private readonly RackService _rackService;
        public ActiveRackService(IGalerySession session, IDatabaseInfoRepository databaseInfoRepository, RackService rackService)
        {
            _session = session;
            _databaseInfoRepository = databaseInfoRepository;
            _rackService = rackService;
        }

        private DatabaseInfo _database;
        public DatabaseInfo ActiveDatabase => _database ??= _databaseInfoRepository.Get(_session.ActiveDatabaseHash);

        private Rack _rack;
        public Rack ActiveRack => _rack ??= ActiveDatabase.Racks.First(r => r.Hash == _session.ActiveRackHash);

        public string ActiveDatabaseName => ActiveDatabase.Name;
        
        public string ActiveDirectory => _rackService.GetActiveDirectory(ActiveRack);

        public string ActiveRackHash => ActiveRack.Hash;

        public string ActiveRackName => ActiveRack.Name;

        public string GetSubpath(string fullPath)
        {
            return _rackService.GetSubpath(ActiveRack, fullPath);
        }
    }
}
