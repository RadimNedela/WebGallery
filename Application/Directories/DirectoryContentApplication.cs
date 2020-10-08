using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;
using Domain.Elements;
using Domain.InfrastructureInterfaces;
using Domain.Logging;
using Domain.Services;

namespace Application.Directories
{
    public class DirectoryContentApplication
    {
        private static readonly ISimpleLogger log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DirectoryContentBuilder _directoryContentBuilder;
        private readonly IContentEntityRepository _contentRepository;

        public DirectoryContentApplication(
            DirectoryContentBuilder directoryContentBuilder,
            IContentEntityRepository contentRepository)
        {
            _directoryContentBuilder = directoryContentBuilder;
            _contentRepository = contentRepository;
        }

        public DisplayableInfoDto GetDirectoryContent(string path)
        {
            log.Begin($"{nameof(GetDirectoryContent)}.{path}");
            
            var directoryBinder = _directoryContentBuilder.GetDirectoryContent(path);
            PersistDirectoryContent(directoryBinder);
            var retVal = directoryBinder.ToDisplayableInfoDto();
            
            log.End($"{nameof(GetDirectoryContent)}.{path}");
            return retVal;
        }

        private void PersistDirectoryContent(BinderElement directoryBinder)
        {
            log.Begin($"{nameof(PersistDirectoryContent)}.{directoryBinder}");

            foreach (var content in directoryBinder.Contents)
            {
                _contentRepository.Add(content.ToEntity());
            }
            _contentRepository.Save();

            log.End($"{nameof(PersistDirectoryContent)}.{directoryBinder}");
        }
    }
}