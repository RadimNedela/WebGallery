using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebGalery.Core;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Logging;
using WebGalery.FileImport.Application.Dtos;
using WebGalery.FileImport.Application.Dtos.Directories;
using WebGalery.FileImport.Services;
using WebGalery.Maintenance.Domain;

namespace WebGalery.FileImport.Application
{
    public class DirectoryContentApplication
    {
        //private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //private readonly DirectoryContentBuilder _directoryContentBuilder;
        //private readonly IContentEntityRepository _contentRepository;
        private readonly IGalerySession session;
        private readonly CurrentDatabaseInfoProvider dbInfoProvider;
        private readonly IDirectoryMethods directoryMethods;


        public DirectoryContentApplication(
            IGalerySession session,
            CurrentDatabaseInfoProvider dbInfoProvider,
            //DirectoryContentBuilder directoryContentBuilder,
            //IContentEntityRepository contentRepository,
            IDirectoryMethods directoryMethods
            )
        {
            this.session = session;
            this.dbInfoProvider = dbInfoProvider;
            //_directoryContentBuilder = directoryContentBuilder;
            //_contentRepository = contentRepository;
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

        private DirectoryInfoDto GetSubDirectoryInfo(string subDirectory)
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
            //var activeDirectory = databaseInfo.GetActiveDirectory();
            //var fullPath = Path.Combine(activeDirectory, subDirectory);

            //if (DirectoryContentInfos.ContentInfos.ContainsKey(fullPath))
            //    return DirectoryContentInfos.ContentInfos[fullPath];
            return null;
        }

        public async Task<DirectoryContentThreadInfoDto> ParseDirectoryContentAsync(string subDirectory)
        {
            throw new NotImplementedException();
            //var activeDirectory = GetActiveDirectory();
            //var fullPath = Path.Combine(activeDirectory, subDirectory);

            //var info = new DirectoryContentThreadInfo { FullPath = fullPath };
            //DirectoryContentInfos.ContentInfos.Add(fullPath, info);
            //await Task.Run(() => GetDirectoryContent(info));

            //var retVal = GetThreadInfo(subDirectory);
            //DirectoryContentInfos.ContentInfos.Remove(fullPath);
            //return retVal;
        }

        public DisplayableInfoDto GetDirectoryContent(string path)
        {
            throw new NotImplementedException();
            //return GetDirectoryContent(new DirectoryContentThreadInfo { FullPath = path });
        }

        //private DisplayableInfoDto GetDirectoryContent(DirectoryContentThreadInfo info)
        //{
        //    Log.Begin($"{nameof(GetDirectoryContent)}.{info.FullPath}");

        //    var directoryBinder = _directoryContentBuilder.GetDirectoryContent(info);
        //    PersistDirectoryContent(directoryBinder);
        //    var retVal = directoryBinder.ToDisplayableInfoDto();

        //    Log.End($"{nameof(GetDirectoryContent)}.{info.FullPath}");
        //    return retVal;
        //}

        //private void PersistDirectoryContent(BinderElement directoryBinder)
        //{
        //    Log.Begin($"{nameof(PersistDirectoryContent)}.{directoryBinder}");

        //    foreach (var content in directoryBinder.Contents)
        //    {
        //        _contentRepository.Add(content.ToEntity());
        //    }
        //    _contentRepository.Save();

        //    Log.End($"{nameof(PersistDirectoryContent)}.{directoryBinder}");
        //}
    }
}