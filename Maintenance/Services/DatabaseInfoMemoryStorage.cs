using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DbEntities.Maintenance;
using Domain.Elements.Maintenance;
using Domain.Services.InfrastructureInterfaces;

namespace Domain.Services
{
    public class DatabaseInfoMemoryStorage2 : IDatabaseInfoElementRepository
    {
        private IList<DatabaseInfoElement> _allInfos;
        private readonly IDatabaseInfoEntityRepository _repository;
        private readonly IHasher _hasher;

        public DatabaseInfoMemoryStorage2(
            IDatabaseInfoEntityRepository repository,
            IHasher hasher)
        {
            _repository = repository;
            _hasher = hasher;
            Initialize();
        }

        public DatabaseInfoElement First(Func<DatabaseInfoElement, bool> predicate)
        {
            var currentDatabaseInfo = _allInfos.FirstOrDefault(predicate);
            if (currentDatabaseInfo == null)
            {
                Initialize();
                currentDatabaseInfo = _allInfos.First();
            }
            return currentDatabaseInfo;
        }

        public void Initialize()
        {
            IEnumerable<DatabaseInfoEntity> allEntities = _repository.GetAll();
            _allInfos = allEntities.Select(e => new DatabaseInfoElement(_hasher, e)).ToList();
        }
    }
}
