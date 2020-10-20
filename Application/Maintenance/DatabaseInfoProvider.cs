using Domain.DbEntities.Maintenance;
using Domain.Elements.Maintenance;
using Domain.InfrastructureInterfaces;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Controllers
{
    public class DatabaseInfoProvider : IDatabaseInfoProvider
    {
        private IList<DatabaseInfoElement> _allInfos;
        public DatabaseInfoElement CurrentDatabaseInfo { get; set; }

        public RackElement CurrentRack { get; set; }

        public DatabaseInfoProvider(IDatabaseInfoEntityRepository repository, IHasher hasher)
        {
            IEnumerable<DatabaseInfoEntity> allEntities = repository.GetAll();
            _allInfos = allEntities.Select(e => new DatabaseInfoElement(hasher, e)).ToList();
            if (_allInfos.Any())
                CurrentDatabaseInfo = _allInfos.First();
            else
                CurrentDatabaseInfo = new DatabaseInfoElement(hasher, "Default");

            CurrentRack = CurrentDatabaseInfo.Racks.Last();
        }
    }
}
