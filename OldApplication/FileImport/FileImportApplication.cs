using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WebGalery.Application.FileImport.Helpers;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.FileImport;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.InfrastructureInterfaces.Base;
using WebGalery.Core.Logging;
using WebGalery.Core.Maintenance;

namespace WebGalery.Application.FileImport
{
    public class FileImportApplication
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        private readonly PhysicalFilesParser _physicalFilesParser;
        private readonly PhysicalFileToContentConverter _physicalFileToContentConverter;
        private readonly IPersister<Content> _contentEntityPersister;
        private readonly RackInfoBuilder _rackInfoBuilder;
        private readonly IActiveRackService _dbInfoProvider;
        private readonly IDirectoryMethods _directoryMethods;
        private readonly DirectoryContentThreadInfoFactory _directoryContentThreadInfoFactory;

        public FileImportApplication(
            RackInfoBuilder rackInfoBuilder,
            IActiveRackService dbInfoProvider,
            PhysicalFilesParser physicalFilesParser,
            IPersister<Content> contentEntityPersister,
            IDirectoryMethods directoryMethods,
            PhysicalFileToContentConverter physicalFileToContentConverter, 
            DirectoryContentThreadInfoFactory directoryContentThreadInfoFactory)
        {
            _rackInfoBuilder = rackInfoBuilder;
            _dbInfoProvider = dbInfoProvider;
            _physicalFilesParser = physicalFilesParser;
            _contentEntityPersister = contentEntityPersister;
            _directoryMethods = directoryMethods;
            _physicalFileToContentConverter = physicalFileToContentConverter;
            _directoryContentThreadInfoFactory = directoryContentThreadInfoFactory;
        }

        public RackInfoDto GetCurrentRackInfo()
        {
            return _rackInfoBuilder.GetCurrentRackInfo();
        }

        public DirectoryInfoDto GetSubDirectoryInfo(string subDirectory)
        {
            return _rackInfoBuilder.GetSubDirectoryInfo(subDirectory);
        }

        public DirectoryContentThreadInfoDto GetThreadInfo(string subDirectory)
        {
            var fullPath = GetFullPath(subDirectory);
            if (DirectoryContentInfos.ContentInfos.ContainsKey(fullPath))
                return DirectoryContentInfos.ContentInfos[fullPath].ThreadInfoDto;
            return null;
        }

        public async Task<DirectoryContentThreadInfoDto> ParseDirectoryContentAsync(string subDirectory)
        {
            var fullPath = GetFullPath(subDirectory);
            var info = _directoryContentThreadInfoFactory.Build(fullPath);
            DirectoryContentInfos.ContentInfos.Add(fullPath, info);
            await Task.Run(() => DoParseDirectoryContent(info));

            var retVal = GetThreadInfo(subDirectory);
            DirectoryContentInfos.ContentInfos.Remove(fullPath);
            return retVal;
        }

        public DirectoryContentThreadInfoDto ParseDirectoryContent(string subDirectory)
        {
            var fullPath = GetFullPath(subDirectory);
            var info = _directoryContentThreadInfoFactory.Build(fullPath);
            DoParseDirectoryContent(info);
            return info.ThreadInfoDto;
        }

        private string GetFullPath(string subDirectory)
        {
            var activeDirectory = _dbInfoProvider.ActiveDirectory;
            var fullPath = Path.Combine(activeDirectory, subDirectory);
            return fullPath;
        }

        private void DoParseDirectoryContent(DirectoryContentThreadInfo info)
        {
            Log.Begin($"{nameof(DoParseDirectoryContent)}.{info.FullPath}");

            foreach (var file in _physicalFilesParser.ParsePhysicalFiles(info))
            {
                PersistContentEntity(_physicalFileToContentConverter.ToContentEntity(file));
            }

            Log.End($"{nameof(DoParseDirectoryContent)}.{info.FullPath}");
        }

        private void PersistContentEntity(Content contentEntity)
        {
            Log.Begin($"{nameof(PersistContentEntity)}.{contentEntity}");

            _contentEntityPersister.Add(contentEntity);
            _contentEntityPersister.Save();

            Log.End($"{nameof(PersistContentEntity)}.{contentEntity}");
        }
    }
}