using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.Maintenance
{
    public class CurrentDatabaseInfoProvider : ICurrentDatabaseInfoProvider
    {
        private readonly IGalerySession _session;
        private readonly IDatabaseInfoEntityRepository _repository;

        public CurrentDatabaseInfoProvider(IGalerySession session, IDatabaseInfoEntityRepository repository)
        {
            _session = session;
            _repository = repository;
        }

        private DatabaseInfo _databaseInfo;
        public IDatabaseInfo CurrentInfo => _databaseInfo ??= new DatabaseInfo(_repository.Get(_session.CurrentDatabaseHash), _session.CurrentRackHash);
    }
}
