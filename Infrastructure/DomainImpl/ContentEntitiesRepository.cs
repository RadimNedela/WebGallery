using System.Linq;
using Domain.DbEntities;
using Domain.InfrastructureInterfaces;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainImpl
{
    public class ContentEntitiesRepository : IContentEntityRepository
    {
        private readonly IGaleryDatabase _galeryDatabase;

        public ContentEntitiesRepository(IGaleryDatabase galeryDatabase)
        {
            _galeryDatabase = galeryDatabase;
        }

        public void Add(ContentEntity contentEntity)
        {
            _galeryDatabase.Contents.Add(contentEntity);
        }

        public void Save()
        {
            _galeryDatabase.SaveChanges();
        }

        public ContentEntity Get(string hash)
        {
            return _galeryDatabase.Contents
                .Where(h => h.Hash == hash)
                    .Include(ce => ce.Binders)
                        .ThenInclude(b => b.Binder)
                    .Include(he => he.AttributedBinders)
                        .ThenInclude(ab => ab.AttributedBinder)
                    .FirstOrDefault();
        }
    }
}