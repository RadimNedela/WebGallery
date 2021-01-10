using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebGalery.Core;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Logging;
using WebGalery.FileImport.Application.Dtos;
using WebGalery.FileImport.Application.Dtos.Directories;
using WebGalery.FileImport.Domain;
using WebGalery.Maintenance.Domain;

namespace WebGalery.FileImport.Application
{
    public class DirectoryContentApplication
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        private readonly IDirectoryContentBuilder directoryContentBuilder;
        private readonly IContentEntityRepository contentRepository;
        private readonly IGalerySession session;
        private readonly CurrentDatabaseInfoProvider dbInfoProvider;
        private readonly IDirectoryMethods directoryMethods;


        public DirectoryContentApplication(
            IGalerySession session,
            CurrentDatabaseInfoProvider dbInfoProvider,
            IDirectoryContentBuilder directoryContentBuilder,
            IContentEntityRepository contentRepository,
            IDirectoryMethods directoryMethods
            )
        {
            this.session = session;
            this.dbInfoProvider = dbInfoProvider;
            this.directoryContentBuilder = directoryContentBuilder;
            this.contentRepository = contentRepository;
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

        public DirectoryContentThreadInfoDto GetThreadInfo(string subDirectory)
        {
            var fullPath = GetFullPath(subDirectory);
            if (DirectoryContentInfos.ContentInfos.ContainsKey(fullPath))
                return DirectoryContentInfos.ContentInfos[fullPath];
            return null;
        }

        public async Task<DirectoryContentThreadInfoDto> ParseDirectoryContentAsync(string subDirectory)
        {
            var fullPath = GetFullPath(subDirectory);
            var info = new DirectoryContentThreadInfo { FullPath = fullPath };
            DirectoryContentInfos.ContentInfos.Add(fullPath, info);
            await Task.Run(() => ParseDirectoryContent(info));

            var retVal = GetThreadInfo(subDirectory);
            DirectoryContentInfos.ContentInfos.Remove(fullPath);
            return retVal;
        }

        private string GetFullPath(string subDirectory)
        {
            var activeDirectory = dbInfoProvider.CurrentInfo.CurrentRack.ActiveDirectory;
            var fullPath = Path.Combine(activeDirectory, subDirectory);
            return fullPath;
        }

        public DirectoryContentThreadInfoDto ParseDirectoryContent(string path)
        {
            var info = new DirectoryContentThreadInfo {FullPath = path};
            ParseDirectoryContent(info);
            return info;
        }

        private void ParseDirectoryContent(DirectoryContentThreadInfo info)
        {
            Log.Begin($"{nameof(ParseDirectoryContent)}.{info.FullPath}");

            foreach (PhysicalFile file in directoryContentBuilder.PhysicalFiles())
            {
                PersistPhysicalFile(file);
            }

            Log.End($"{nameof(ParseDirectoryContent)}.{info.FullPath}");
        }

        private void PersistPhysicalFile(PhysicalFile physicalFile)
        {
            Log.Begin($"{nameof(PersistPhysicalFile)}.{physicalFile}");

            contentRepository.Add(physicalFile.ContentEntity);
            contentRepository.Save();

            Log.End($"{nameof(PersistPhysicalFile)}.{physicalFile}");
        }
    }
}