using System.Linq;
using Domain.Entities;
using Domain.InfrastructureInterfaces;
using Infrastructure.MySqlDb;

namespace Infrastructure.DomainImpl
{
    public class HashedEntitiesRepository : IHashedEntitiesRepository
    {
        private readonly MySqlDbContext _dbContext;

        public HashedEntitiesRepository(MySqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Hashed hashed)
        {
            _dbContext.Hashed.Add(hashed);
        }

        public Hashed Get(string hash)
        {
            return _dbContext.Hashed.SingleOrDefault(he => he.Hash == hash);
        }
    }
}