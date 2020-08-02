using System.Linq;
using Domain.DbEntities;
using Domain.InfrastructureInterfaces;
using Infrastructure.MySqlDb;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainImpl
{
    public class HashedEntitiesRepository : IHashedEntitiesRepository
    {
        private readonly MySqlDbContext _dbContext;

        public HashedEntitiesRepository(MySqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(HashedEntity hashed)
        {
            _dbContext.Hashes.Add(hashed);
        }

        public HashedEntity Get(string hash)
        {
            return _dbContext.Hashes
                .Where(h => h.Hash == hash)
                    .Include(he => he.Locations)
                        .ThenInclude(l => l.Location)
                        .ThenInclude(l => l.Tags)
                        .ThenInclude(t => t.Tag)
                    .Include(he => he.Tags)
                        .ThenInclude(t => t.Tag)
                    .FirstOrDefault();

        }
    }
}