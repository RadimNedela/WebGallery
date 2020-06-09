using System.Linq;
using Domain.Elements;
using Domain.InfrastructureInterfaces;
using Infrastructure.MySqlDb;

namespace Infrastructure.DomainImpl
{
    public class HashedElementsRepository : IHashedElementsRepository
    {
        private readonly MySqlDbContext _dbContext;

        public HashedElementsRepository(MySqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HashedElement GetHashedElement(string hash)
        {
            return _dbContext.HashedElements.Single(he => he.Hash == hash);
        }
    }
}