using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces.Base;
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
        private readonly IEntityPersister<ContentEntity> contentEntityPersister;
        private readonly RackInfoBuilder rackInfoBuilder;
        private readonly ICurrentDatabaseInfoProvider dbInfoProvider;

        public FileImportApplication(
            RackInfoBuilder rackInfoBuilder,
            ICurrentDatabaseInfoProvider dbInfoProvider,
            PhysicalFilesParser physicalFilesParser,
            IEntityPersister<ContentEntity> contentEntityPersister
            )
        {
            this.rackInfoBuilder = rackInfoBuilder;
            this.dbInfoProvider = dbInfoProvider;
            this.physicalFilesParser = physicalFilesParser;
            this.contentEntityPersister = contentEntityPersister;
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
            await Task.Run(() => DoParseDirectoryContent(info));

            var retVal = GetThreadInfo(subDirectory);
            DirectoryContentInfos.ContentInfos.Remove(fullPath);
            return retVal;
        }

        public DirectoryContentThreadInfoDto ParseDirectoryContent(string subDirectory)
        {
            var fullPath = GetFullPath(subDirectory);
            var info = new DirectoryContentThreadInfo { FullPath = fullPath };
            DoParseDirectoryContent(info);
            return info;
        }

        private string GetFullPath(string subDirectory)
        {
            var activeDirectory = dbInfoProvider.CurrentInfo.ActiveRack.ActiveDirectory;
            var fullPath = Path.Combine(activeDirectory, subDirectory);
            return fullPath;
        }

        private void DoParseDirectoryContent(DirectoryContentThreadInfo info)
        {
            Log.Begin($"{nameof(DoParseDirectoryContent)}.{info.FullPath}");

            foreach (ContentEntity entity in physicalFilesParser.ParsePhysicalFiles(info))
            {
                PersistContentEntity(entity);
            }

            Log.End($"{nameof(DoParseDirectoryContent)}.{info.FullPath}");
        }

        private void PersistContentEntity(ContentEntity contentEntity)
        {
            Log.Begin($"{nameof(PersistContentEntity)}.{contentEntity}");

            contentEntityPersister.Add(contentEntity);
            contentEntityPersister.Save();

            Log.End($"{nameof(PersistContentEntity)}.{contentEntity}");
        }
    }
}