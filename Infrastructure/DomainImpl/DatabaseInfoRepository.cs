using System.Linq;
using Domain.DbEntities.Maintenance;
using Domain.InfrastructureInterfaces;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainImpl
{
    public class DatabaseInfoRepository : EntitiesRepository<DatabaseInfoEntity>, IDatabaseInfoEntityRepository
    {
        public DatabaseInfoRepository(IGaleryDatabase galeryDatabase)
            : base(galeryDatabase) { }

        protected override DbSet<DatabaseInfoEntity> TheDbSet => _galeryDatabase.DatabaseInfo;

        public DatabaseInfoEntity Get(string hash)
        {
            return _galeryDatabase.DatabaseInfo
                .Where(h => h.Hash == hash)
                    .Include(di => di.Racks)
                    .Include(di => di.MountPoints)
                    .FirstOrDefault();
        }
    }
}