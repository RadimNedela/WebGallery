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

        private DatabaseInfoEntity _entity;
        private DatabaseInfoEntity Entity => _entity ??= repository.Get(session.CurrentDatabaseHash);

        public string CurrentDatabaseInfoName => Entity.Name;

        public RackEntity CurrentRack => Entity.Racks.First(r => r.Hash == session.CurrentRackHash);

        public string CurrentRackName => CurrentRack.Name;

        public string ActiveDirectory => CurrentRack.MountPoints.First(mp => Directory.Exists(mp.Path)).Path;

        public DatabaseInfo(IGalerySession session,
            IDatabaseInfoEntityRepository repository)
        {
            this.session = session;
            this.repository = repository;
        }
    }
}
