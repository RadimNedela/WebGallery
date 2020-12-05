using System.Collections.Generic;
using System.Linq;
using Domain.DbEntities.Maintenance;
using Domain.Services.InfrastructureInterfaces;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainImpl
{
    public class DatabaseInfoRepository : EntitiesRepository<DatabaseInfoEntity>, IDatabaseInfoEntityRepository
    {
        public DatabaseInfoRepository(IGaleryDatabase galeryDatabase)
            : base(galeryDatabase) { }

        protected override DbSet<DatabaseInfoEntity> TheDbSet => GaleryDatabase.DatabaseInfo;

        public DatabaseInfoEntity Get(string hash)
        {
            return GaleryDatabase.DatabaseInfo
                .Where(h => h.Hash == hash)
                    .Include(di => di.Racks)
                        .ThenInclude(r => r.MountPoints)
                    .FirstOrDefault();
        }

        public IEnumerable<DatabaseInfoEntity> GetAll()
        {
            return GaleryDatabase.DatabaseInfo
                .Include(di => di.Racks)
                    .ThenInclude(r => r.MountPoints);
        }
    }
}