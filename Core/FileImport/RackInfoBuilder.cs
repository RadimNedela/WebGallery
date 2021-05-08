using System.IO;
using System.Linq;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.FileImport
{
    public class RackInfoBuilder
    {
        private readonly IGalerySession _session;
        private readonly IActiveRackService _activeRackService;
        private readonly IDirectoryMethods _directoryMethods;

        public RackInfoBuilder(
            IGalerySession session,
            IActiveRackService activeRackService,
            IDirectoryMethods directoryMethods
            )
        {
            _session = session;
            _activeRackService = activeRackService;
            _directoryMethods = directoryMethods;
        }

        public RackInfoDto GetCurrentRackInfo()
        {
            var retVal = new RackInfoDto
            {
                ActiveDatabaseName = _activeRackService.ActiveDatabaseName,
                ActiveDatabaseHash = _session.ActiveDatabaseHash,
                ActiveRackName = _activeRackService.ActiveRackName,
                ActiveRackHash = _session.ActiveRackHash,
                ActiveDirectory = _activeRackService.ActiveDirectory,
                DirectoryInfo = GetSubDirectoryInfo(".")
            };

            return retVal;
        }

        public DirectoryInfoDto GetSubDirectoryInfo(string subDirectory)
        {
            var directoryInfo = new DirectoryInfoDto();
            var activeDirectory = _activeRackService.ActiveDirectory;
            var fullPath = Path.Combine(activeDirectory, subDirectory);

            var fileNames = _directoryMethods.GetFileNames(fullPath).Select(Path.GetFileName);
            var dirNames = _directoryMethods.GetDirectories(fullPath).Select(path => _activeRackService.GetSubpath(path));

            var normSubDir = _activeRackService.GetSubpath(fullPath);
            if (normSubDir != ".")
                dirNames = new[] { Path.Combine(normSubDir, @"..") }.Union(dirNames);

            directoryInfo.SubDirectories = dirNames;
            directoryInfo.Files = fileNames;
            directoryInfo.CurrentDirectory = subDirectory;

            return directoryInfo;
        }
    }
}
