using System.IO;
using System.Linq;
using WebGalery.Core;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.FileImport.Dtos;
using WebGalery.Maintenance.Domain;

namespace WebGalery.FileImport.Domain
{
    public class RackInfoBuilder
    {
        private readonly IGalerySession session;
        private readonly CurrentDatabaseInfoProvider dbInfoProvider;
        private readonly IDirectoryMethods directoryMethods;

        public RackInfoBuilder(
            IGalerySession session,
            CurrentDatabaseInfoProvider dbInfoProvider,
            IDirectoryMethods directoryMethods
            )
        {
            this.session = session;
            this.dbInfoProvider = dbInfoProvider;
            this.directoryMethods = directoryMethods;
        }

        public RackInfoDto GetCurrentRackInfo()
        {
            var retVal = new RackInfoDto
            {
                ActiveDatabaseName = dbInfoProvider.CurrentInfo.CurrentDatabaseInfoName,
                ActiveDatabaseHash = session.CurrentDatabaseHash,
                ActiveRackName = dbInfoProvider.CurrentInfo.CurrentRack.Name,
                ActiveRackHash = session.CurrentRackHash,
                ActiveDirectory = dbInfoProvider.CurrentInfo.CurrentRack.ActiveDirectory,
                DirectoryInfo = GetSubDirectoryInfo(".")
            };

            return retVal;
        }

        public DirectoryInfoDto GetSubDirectoryInfo(string subDirectory)
        {
            var rack = dbInfoProvider.CurrentInfo.CurrentRack;
            var directoryInfo = new DirectoryInfoDto();
            var activeDirectory = rack.ActiveDirectory;
            var fullPath = Path.Combine(activeDirectory, subDirectory);

            var fileNames = directoryMethods.GetFileNames(fullPath).Select(Path.GetFileName);
            var dirNames = directoryMethods.GetDirectories(fullPath).Select(path => rack.GetSubpath(path));

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
