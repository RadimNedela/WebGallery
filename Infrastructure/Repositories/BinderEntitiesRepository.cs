using System.Linq;
using Domain.DbEntities;
using Domain.Services.InfrastructureInterfaces;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainImpl
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