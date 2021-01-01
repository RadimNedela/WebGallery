using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Logging;
using WebGalery.FileImport.Application.Dtos;
using WebGalery.FileImport.Application.Dtos.Directories;
using WebGalery.FileImport.Services;

namespace WebGalery.FileImport.Application
{
    public class DirectoryContentApplication
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DirectoryContentBuilder _directoryContentBuilder;
        private readonly IContentEntityRepository _contentRepository;
        private readonly IDatabaseInfoProvider _databaseInfoProvider;
        private readonly IDirectoryMethods _directoryMethods;

        public DirectoryContentApplication(
            IDatabaseInfoProvider databaseInfoProvider,
            DirectoryContentBuilder directoryContentBuilder,
            IContentEntityRepository contentRepository,
            IDirectoryMethods directoryMethods)
        {
            _directoryContentBuilder = directoryContentBuilder;
            _contentRepository = contentRepository;
            _databaseInfoProvider = databaseInfoProvider;
            _directoryMethods = directoryMethods;
        }

        public RackInfoDto GetCurrentRackInfo()
        {
            var db = _databaseInfoProvider.CurrentDatabaseInfo;
            var rack = _databaseInfoProvider.CurrentRack;
            var activeDirectory = GetActiveDirectory(rack.MountPoints);

            var retVal = new RackInfoDto
            {
                ActiveDatabaseName = db.Name,
                ActiveDatabaseHash = db.Hash,
                ActiveRackName = rack.Name,
                ActiveRackHash = rack.Hash,
                ActiveDirectory = activeDirectory,
                DirectoryInfo = GetSubDirectoryInfo(".")
            };


            return retVal;
        }

        public DirectoryInfoDto GetSubDirectoryInfo(string subDirectory)
        {
            var rack = _databaseInfoProvider.CurrentRack;

            var directoryInfo = new DirectoryInfoDto();
            var activeDirectory = GetActiveDirectory();
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

        public string GetActiveDirectory()
        {
            return GetActiveDirectory(_databaseInfoProvider.CurrentRack.MountPoints);
        }

        public string GetActiveDirectory(IList<string> mountPoints)
        {
            foreach (var t in mountPoints)
            {
                if (_directoryMethods.DirectoryExists(t))
                    return t;
            }

            throw new Exception("Sorry, the rack cannot be used because no mount point is currently mounted");
        }

        public DirectoryContentThreadInfoDto GetThreadInfo(string subDirectory)
        {
            var activeDirectory = GetActiveDirectory();
            var fullPath = Path.Combine(activeDirectory, subDirectory);

            if (DirectoryContentInfos.ContentInfos.ContainsKey(fullPath))
                return DirectoryContentInfos.ContentInfos[fullPath];
            return null;
        }

        public async Task<DirectoryContentThreadInfoDto> ParseDirectoryContentAsync(string subDirectory)
        {
            var activeDirectory = GetActiveDirectory();
            var fullPath = Path.Combine(activeDirectory, subDirectory);

            var info = new DirectoryContentThreadInfo { FullPath = fullPath };
            DirectoryContentInfos.ContentInfos.Add(fullPath, info);
            await Task.Run(() => GetDirectoryContent(info));

            var retVal = GetThreadInfo(subDirectory);
            DirectoryContentInfos.ContentInfos.Remove(fullPath);
            return retVal;
        }

        public DisplayableInfoDto GetDirectoryContent(string path)
        {
            return GetDirectoryContent(new DirectoryContentThreadInfo { FullPath = path });
        }

        private DisplayableInfoDto GetDirectoryContent(DirectoryContentThreadInfo info)
        {
            Log.Begin($"{nameof(GetDirectoryContent)}.{info.FullPath}");

            var directoryBinder = _directoryContentBuilder.GetDirectoryContent(info);
            PersistDirectoryContent(directoryBinder);
            var retVal = directoryBinder.ToDisplayableInfoDto();

            Log.End($"{nameof(GetDirectoryContent)}.{info.FullPath}");
            return retVal;
        }

        private void PersistDirectoryContent(BinderElement directoryBinder)
        {
            Log.Begin($"{nameof(PersistDirectoryContent)}.{directoryBinder}");

            foreach (var content in directoryBinder.Contents)
            {
                _contentRepository.Add(content.ToEntity());
            }
            _contentRepository.Save();

            Log.End($"{nameof(PersistDirectoryContent)}.{directoryBinder}");
        }
    }

    public interface IDatabaseInfoProvider
    {
        DatabaseInfoEntity CurrentDatabaseInfo { get; }
        RackEntity CurrentRack { get; }
    }
}