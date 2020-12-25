using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Infrastructure.Databases;
using WebGalery.Infrastructure.Repositories.Base;

namespace WebGalery.Infrastructure.Repositories
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