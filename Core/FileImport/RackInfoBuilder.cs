using System.IO;
using System.Linq;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.FileImport
{
    public class RackInfoBuilder
    {
        private readonly IGalerySession _session;
        private readonly ICurrentDatabaseInfoProvider _dbInfoProvider;
        private readonly IDirectoryMethods _directoryMethods;

        public RackInfoBuilder(
            IGalerySession session,
            ICurrentDatabaseInfoProvider dbInfoProvider,
            IDirectoryMethods directoryMethods
            )
        {
            _session = session;
            _dbInfoProvider = dbInfoProvider;
            _directoryMethods = directoryMethods;
        }

        public RackInfoDto GetCurrentRackInfo()
        {
            var retVal = new RackInfoDto
            {
                ActiveDatabaseName = _dbInfoProvider.CurrentInfo.CurrentDatabaseInfoName,
                ActiveDatabaseHash = _session.CurrentDatabaseHash,
                ActiveRackName = _dbInfoProvider.CurrentInfo.ActiveRack.Name,
                ActiveRackHash = _session.CurrentRackHash,
                ActiveDirectory = _dbInfoProvider.CurrentInfo.ActiveRack.ActiveDirectory,
                DirectoryInfo = GetSubDirectoryInfo(".")
            };

            return retVal;
        }

        public DirectoryInfoDto GetSubDirectoryInfo(string subDirectory)
        {
            var rack = _dbInfoProvider.CurrentInfo.ActiveRack;
            var directoryInfo = new DirectoryInfoDto();
            var activeDirectory = rack.ActiveDirectory;
            var fullPath = Path.Combine(activeDirectory, subDirectory);

            var fileNames = _directoryMethods.GetFileNames(fullPath).Select(Path.GetFileName);
            var dirNames = _directoryMethods.GetDirectories(fullPath).Select(path => rack.GetSubpath(path));

            var normSubDir = rack.GetSubpath(fullPath);
            if (normSubDir != ".")
                dirNames = new[] { Path.Combine(normSubDir, @"..") }.Union(dirNames);

            directoryInfo.SubDirectories = dirNames;
            directoryInfo.Files = fileNames;
            directoryInfo.CurrentDirectory = subDirectory;

            return directoryInfo;
        }
    }
}
