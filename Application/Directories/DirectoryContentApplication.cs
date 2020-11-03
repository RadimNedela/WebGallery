using Domain.Dtos;
using Domain.Elements;
using Domain.InfrastructureInterfaces;
using Domain.Logging;
using Domain.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application.Directories
{
    public class DirectoryContentApplication
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            var directoryInfo = new DirectoryInfoDto();
            var activeDirectory = GetActiveDirectory();
            var fullPath = Path.Combine(activeDirectory, subDirectory);

            var fileNames = _directoryMethods.GetFileNames(fullPath).Select(path => Path.GetFileName(path));
            var dirNames = _directoryMethods.GetDirectories(fullPath).Select(path => _databaseInfoProvider.CurrentRack.GetSubpath(path));

            directoryInfo.SubDirectories = dirNames;
            directoryInfo.Files = fileNames;

            return directoryInfo;
        }

        public string GetActiveDirectory()
        {
            return GetActiveDirectory(_databaseInfoProvider.CurrentRack.MountPoints);
        }

        public string GetActiveDirectory(IList<string> mountPoints)
        {
            for (var i = 0; i < mountPoints.Count; i++)
            {
                if (_directoryMethods.DirectoryExists(mountPoints[i]))
                    return mountPoints[i];
            }
            throw new Exception("Sorry, the rack cannot be used because no mount point is currently mounted");
        }

        public DisplayableInfoDto GetDirectoryContent(string path)
        {
            Log.Begin($"{nameof(GetDirectoryContent)}.{path}");
            
            var directoryBinder = _directoryContentBuilder.GetDirectoryContent(path);
            PersistDirectoryContent(directoryBinder);
            var retVal = directoryBinder.ToDisplayableInfoDto();
            
            Log.End($"{nameof(GetDirectoryContent)}.{path}");
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
}