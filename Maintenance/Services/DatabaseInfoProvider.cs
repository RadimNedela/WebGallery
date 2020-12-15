using System.Linq;
using Domain.Elements.Maintenance;
using Domain.Services.InfrastructureInterfaces;

namespace Domain.Services
{
    public class DatabaseInfoProvider //: IDatabaseInfoProvider, IDatabaseInfoInitializer
    {
        //private readonly IDatabaseInfoElementRepository _databaseInfoRepository;
        //private readonly IDirectoryMethods _directoryMethods;
        //private readonly IHasher _hasher;
        //private DatabaseInfoElement _currentDatabaseInfo;
        //private RackService _currentRack;

        //public DatabaseInfoElement CurrentDatabaseInfo =>
        //    _currentDatabaseInfo ??= new DatabaseInfoElement(_hasher, "Default",
        //        "Default", _directoryMethods.GetCurrentDirectoryName() + @"\..\..\..\..");

        //public RackService CurrentRack => _currentRack ??= CurrentDatabaseInfo.Racks.Last();

        //public DatabaseInfoProvider(
        //    IDatabaseInfoElementRepository databaseInfoRepository,
        //    IDirectoryMethods directoryMethods,
        //    IHasher hasher)
        //{
        //    _databaseInfoRepository = databaseInfoRepository;
        //    _directoryMethods = directoryMethods;
        //    _hasher = hasher;
        //}

        //public void SetCurrentInfo(string rackHash)
        //{
        //    _currentDatabaseInfo = _databaseInfoRepository.First(die => die.Racks.Any(r => r.Hash == rackHash));
        //    _currentRack = CurrentDatabaseInfo.Racks.First(re => re.Hash == rackHash);
        //}
    }
}
