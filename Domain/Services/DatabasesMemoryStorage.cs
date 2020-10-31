using Domain.DbEntities.Maintenance;
using Domain.Elements.Maintenance;
using Domain.InfrastructureInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class DatabasesMemoryStorage
    {
        private IList<DatabaseInfoElement> _allInfos;
        private IDatabaseInfoEntityRepository _repository;
        private IHasher _hasher;

        public IEnumerable<DatabaseInfoElement> AllInfos => _allInfos;

        public DatabasesMemoryStorage(
            IDatabaseInfoEntityRepository repository,
            IHasher hasher)
        {
            _repository = repository;
            _hasher = hasher;
            Initialize();
        }

        public void Initialize()
        {
            IEnumerable<DatabaseInfoEntity> allEntities = _repository.GetAll();
            _allInfos = allEntities.Select(e => new DatabaseInfoElement(_hasher, e)).ToList();
        }
    }
}
