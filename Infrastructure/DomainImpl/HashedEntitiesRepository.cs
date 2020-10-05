using System.Linq;
using Domain.DbEntities;
using Domain.InfrastructureInterfaces;
using Infrastructure.MySqlDb;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainImpl
{
    public class HashedEntitiesRepository : IContentEntityRepository
    {
        private readonly MySqlDbContext _dbContext;

        public HashedEntitiesRepository(MySqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(ContentEntity contentEntity)
        {
            _dbContext.Contents.Add(contentEntity);
        }

        public ContentEntity Get(string hash)
        {
            return _dbContext.Contents
                .Where(h => h.Hash == hash)
                    .Include(ce => ce.Binders)
                        .ThenInclude(b => b.Binder)
                    .Include(he => he.AttributedBinders)
                        .ThenInclude(ab => ab.AttributedBinder)
                    .FirstOrDefault();
        }
    }
}