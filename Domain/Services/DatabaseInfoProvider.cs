using Domain.Elements.Maintenance;
using Domain.InfrastructureInterfaces;
using System.Linq;

namespace Domain.Services
{
    public class DatabaseInfoProvider : IDatabaseInfoProvider, IDatabaseInfoInitializer
    {
        private DatabasesMemoryStorage _databasesMemoryStorage;
        private IDirectoryMethods _directoryMethods;
        private IHasher _hasher;
        private DatabaseInfoElement _currentDatabaseInfo;
        private RackElement _currentRack;

        public DatabaseInfoElement CurrentDatabaseInfo
        {
            get
            {
                if (_currentDatabaseInfo == null)
                    _currentDatabaseInfo = 
                        new DatabaseInfoElement(_hasher, "Default", "Default", _directoryMethods.GetCurrentDirectoryName() + @"\..\..\..\..");
                return _currentDatabaseInfo;
            }
        }

        public RackElement CurrentRack
        {
            get
            {
                if (_currentRack == null)
                    _currentRack = CurrentDatabaseInfo.Racks.Last();
                return _currentRack;
            }
        }

        public DatabaseInfoProvider(
            DatabasesMemoryStorage databasesMemoryStorage,
            IDirectoryMethods directoryMethods,
            IHasher hasher)
        {
            _databasesMemoryStorage = databasesMemoryStorage;
            _directoryMethods = directoryMethods;
            _hasher = hasher;
        }

        public void SetCurrentInfo(string rackHash)
        {
            _currentDatabaseInfo = _databasesMemoryStorage.AllInfos.FirstOrDefault(die => die.Racks.Any(r => r.Hash == rackHash));
            if (_currentDatabaseInfo == null)
            {
                _databasesMemoryStorage.Initialize();
                _currentDatabaseInfo = _databasesMemoryStorage.AllInfos.First(die => die.Racks.Any(r => r.Hash == rackHash));
            }
            _currentRack = CurrentDatabaseInfo.Racks.First(re => re.Hash == rackHash);
        }
    }
}
