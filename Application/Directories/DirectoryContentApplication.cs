using Domain.Dtos;
using Domain.Elements;
using Domain.InfrastructureInterfaces;
using Domain.Logging;
using Domain.Services;
using System;
using System.Linq;

namespace Application.Directories
{
    public class DirectoryContentApplication
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DirectoryContentBuilder _directoryContentBuilder;
        private readonly IContentEntityRepository _contentRepository;
        private readonly IDatabaseInfoProvider _databaseInfoProvider;

        public DirectoryContentApplication(
            IDatabaseInfoProvider databaseInfoProvider,
            DirectoryContentBuilder directoryContentBuilder,
            IContentEntityRepository contentRepository)
        {
            _directoryContentBuilder = directoryContentBuilder;
            _contentRepository = contentRepository;
            _databaseInfoProvider = databaseInfoProvider;
        }

        public RackInfoDto GetCurrentRackInfo()
        {
            var db = _databaseInfoProvider.CurrentDatabaseInfo;
            var rack = _databaseInfoProvider.CurrentRack;
            return new RackInfoDto
            {
                ActiveDatabaseName = db.Name,
                ActiveDatabaseHash = db.Hash,
                ActiveRackName = rack.Name,
                ActiveRackHash = rack.Hash,
                ActiveDirectory = rack.MountPoints.First(),
            };
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