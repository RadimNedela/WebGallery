using Domain.DbEntities.Maintenance;
using Domain.Dtos.Maintenance;
using Domain.Elements.Maintenance;
using Domain.Services.InfrastructureInterfaces;

namespace Domain.Services
{
    public class DatabaseInfoBuilder
    {
        private readonly IHasher _hasher;
        private readonly IDatabaseInfoEntityRepository _repository;
        private readonly IDirectoryMethods _directoryMethods;

        public DatabaseInfoBuilder(
            IHasher hasher, 
            IDatabaseInfoEntityRepository repository, 
            IDirectoryMethods directoryMethods)
        {
            _hasher = hasher;
            _repository = repository;
            _directoryMethods = directoryMethods;
        }

        public DatabaseInfoElement Create(DatabaseInfoEntity entity)
        {
            return new DatabaseInfoElement(_hasher, entity);
        }

        public DatabaseInfoElement Create(DatabaseInfoDto dto)
        {
            var element = GetDatabase(dto.Hash);
            element.Merge(dto);
            return element;
        }

        public DatabaseInfoElement GetDatabase(string hash)
        {
            var entity = _repository.Get(hash);
            var element = new DatabaseInfoElement(_hasher, entity);
            return element;
        }

        public DatabaseInfoElement BuildNewDatabase(string databaseName)
        {
            var infoElement = new DatabaseInfoElement(_hasher, databaseName, "Default", _directoryMethods.GetCurrentDirectoryName());

            return infoElement;
        }
    }
}
