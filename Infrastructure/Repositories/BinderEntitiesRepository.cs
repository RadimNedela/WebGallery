using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Infrastructure.Databases;
using WebGalery.Infrastructure.Repositories.Base;

namespace WebGalery.Infrastructure.Repositories
{
    public class BinderEntitiesRepository : EntitiesRepository<BinderEntity>, IBinderEntityRepository
    {
        public BinderEntitiesRepository(IGaleryDatabase galeryDatabase)
            : base(galeryDatabase) { }

        protected override DbSet<BinderEntity> TheDbSet => GaleryDatabase.Binders;

        public BinderEntity Get(string hash)
        {
            return GaleryDatabase.Binders
                .Where(h => h.Hash == hash)
                    .Include(ce => ce.Contents)
                        .ThenInclude(b => b.Content)
                    .Include(he => he.AttributedContents)
                        .ThenInclude(ab => ab.Content)
                    .FirstOrDefault();
        }
    }
}