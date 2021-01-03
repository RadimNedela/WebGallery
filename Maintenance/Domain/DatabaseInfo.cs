using System.IO;
using System.Linq;
using WebGalery.Core;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.DomainInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Maintenance.Domain
{
    public class DatabaseInfo : IDatabaseInfo
    {
        private readonly IGalerySession session;
        private readonly IDatabaseInfoEntityRepository repository;

        private DatabaseInfoEntity entity;

        public DatabaseInfo(IGalerySession session,
            IDatabaseInfoEntityRepository repository)
        {
            this.session = session;
            this.repository = repository;
            entity = 
        }

        public string GetActiveDirectory()
        {
            var rack = GetCurrentRack();
            return rack.MountPoints.First(mp => Directory.Exists(mp.Path)).Path;
        }

        public RackEntity GetCurrentRack()
        {
            var infoElement = GetCurrentDatabaseInfo();
            return infoElement.Racks.First(r => r.Hash == session.CurrentRackHash);
        }

        public DatabaseInfoEntity GetCurrentDatabaseInfo()
        {
            return repository.Get(session.CurrentDatabaseHash);
        }
    }
}
