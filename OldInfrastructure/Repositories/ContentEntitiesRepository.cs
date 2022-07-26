using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Infrastructure.Databases;
using WebGalery.Infrastructure.Repositories.Base;

namespace WebGalery.Infrastructure.Repositories
{
    public class ContentEntitiesRepository : EntitiesRepository<Content>, IContentEntityRepository
    {
        public ContentEntitiesRepository(IGaleryDatabase galeryDatabase)
            : base(galeryDatabase) { }

        protected override DbSet<Content> TheDbSet => GaleryDatabase.Contents;

        public Content Get(string hash)
        {
            return GaleryDatabase.Contents
                .Where(h => h.Hash == hash)
                    .Include(ce => ce.Binders)
                        .ThenInclude(b => b.Binder)
                    .Include(he => he.AttributedBinders)
                        .ThenInclude(ab => ab.Binder)
                    .FirstOrDefault();
        }
    }
}