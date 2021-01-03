using WebGalery.Core;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Maintenance.Domain
{
    public class CurrentDatabaseInfoProvider
    {
        private readonly IGalerySession session;
        private readonly IDatabaseInfoEntityRepository repository;

        public CurrentDatabaseInfoProvider(IGalerySession session, IDatabaseInfoEntityRepository repository)
        {
            this.session = session;
            this.repository = repository;
        }

        private DatabaseInfo _databaseInfo;
        public DatabaseInfo CurrentInfo => _databaseInfo ??= new DatabaseInfo(repository.Get(session.CurrentDatabaseHash), session.CurrentRackHash);
    }
}
