using Domain.Dtos;
using Domain.Elements;
using Domain.InfrastructureInterfaces;
using Domain.Logging;
using Domain.Services;
using System;

namespace Application.Directories
{
    public class DirectoryContentApplication
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DirectoryContentBuilder _directoryContentBuilder;
        private readonly IContentEntityRepository _contentRepository;

        public DirectoryContentApplication(
            DirectoryContentBuilder directoryContentBuilder,
            IContentEntityRepository contentRepository)
        {
            _directoryContentBuilder = directoryContentBuilder;
            _contentRepository = contentRepository;
        }

        public RackInfoDto GetRackInfo(string rackHash)
        {
            return new RackInfoDto
            {
                ActiveDatabaseName = "asdf",
                ActiveDatabaseHash = "fdsa",
                ActiveRackName = "rackName",
                ActiveRackHash = rackHash,
                ActiveDirectory = @"c:\cosik",
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