using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Maintenance;
using WebGalery.Infrastructure.Databases;
using WebGalery.Infrastructure.Repositories.Base;

namespace WebGalery.Infrastructure.Repositories
{
    public class DatabaseInfoRepository : EntitiesRepository<DatabaseInfo>, IDatabaseInfoRepository
    {
        public DatabaseInfoRepository(IGaleryDatabase galeryDatabase)
            : base(galeryDatabase) { }

        protected override DbSet<DatabaseInfo> TheDbSet => GaleryDatabase.DatabaseInfo;

        public DatabaseInfo Get(string hash)
        {
            return GaleryDatabase.DatabaseInfo
                .Where(h => h.Hash == hash)
                    .Include(di => di.Racks)
                        .ThenInclude(r => r.MountPoints)
                    .FirstOrDefault();
        }

        public IEnumerable<DatabaseInfo> GetAll()
        {
            return GaleryDatabase.DatabaseInfo
                .Include(di => di.Racks)
                    .ThenInclude(r => r.MountPoints);
        }
    }
}