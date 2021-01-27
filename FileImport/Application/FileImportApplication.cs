using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Logging;
using WebGalery.FileImport.Application.Helpers;
using WebGalery.FileImport.Domain;
using WebGalery.FileImport.Dtos;

namespace WebGalery.FileImport.Application
{
    public class FileImportApplication
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        private readonly PhysicalFilesParser physicalFilesParser;
        private readonly IContentEntityRepository contentRepository;
        private readonly RackInfoBuilder rackInfoBuilder;
        private readonly ICurrentDatabaseInfoProvider dbInfoProvider;

        public FileImportApplication(
            RackInfoBuilder rackInfoBuilder,
            ICurrentDatabaseInfoProvider dbInfoProvider,
            PhysicalFilesParser physicalFilesParser,
            IContentEntityRepository contentRepository
            )
        {
            this.rackInfoBuilder = rackInfoBuilder;
            this.dbInfoProvider = dbInfoProvider;
            this.physicalFilesParser = physicalFilesParser;
            this.contentRepository = contentRepository;
        }

        public RackInfoDto GetCurrentRackInfo()
        {
            return rackInfoBuilder.GetCurrentRackInfo();
        }

        public DirectoryInfoDto GetSubDirectoryInfo(string subDirectory)
        {
            return rackInfoBuilder.GetSubDirectoryInfo(subDirectory);
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
            var activeDirectory = dbInfoProvider.CurrentInfo.ActiveRack.ActiveDirectory;
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

            foreach (ContentEntity entity in physicalFilesParser.ParsePhysicalFiles(info))
            {
                PersistContentEntity(entity);
            }

            Log.End($"{nameof(ParseDirectoryContent)}.{info.FullPath}");
        }

        private void PersistContentEntity(ContentEntity contentEntity)
        {
            Log.Begin($"{nameof(PersistContentEntity)}.{contentEntity}");

            //contentRepository.Add(physicalFile.ContentEntity);
            //contentRepository.Save();

            Log.End($"{nameof(PersistContentEntity)}.{contentEntity}");
        }
    }
}