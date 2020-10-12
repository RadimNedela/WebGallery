using System.Linq;
using Domain.DbEntities;
using Domain.InfrastructureInterfaces;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainImpl
{
    public class ContentEntitiesRepository : EntitiesRepository<ContentEntity>, IContentEntityRepository
    {
        public ContentEntitiesRepository(IGaleryDatabase galeryDatabase)
            : base(galeryDatabase) { }

        protected override DbSet<ContentEntity> TheDbSet => _galeryDatabase.Contents;

        public ContentEntity Get(string hash)
        {
            return _galeryDatabase.Contents
                .Where(h => h.Hash == hash)
                    .Include(ce => ce.Binders)
                        .ThenInclude(b => b.Binder)
                    .Include(he => he.AttributedBinders)
                        .ThenInclude(ab => ab.Binder)
                    .FirstOrDefault();
        }
    }
}