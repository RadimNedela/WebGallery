using System.Linq;
using Domain.DbEntities;
using Domain.InfrastructureInterfaces;
using Infrastructure.Databases.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainImpl
{
    public class ContentEntitiesRepository : IContentEntityRepository
    {
        private readonly SqlServerDbContext _dbContext;

        public ContentEntitiesRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(ContentEntity contentEntity)
        {
            _dbContext.Contents.Add(contentEntity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
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