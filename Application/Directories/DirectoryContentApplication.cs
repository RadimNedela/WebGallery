using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;
using Domain.Elements;
using Domain.Logging;
using Domain.Services;

namespace Application.Directories
{
    public class DirectoryContentApplication
    {
        private static readonly ISimpleLogger log = new Log4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DirectoryContentBuilder _directoryContentBuilder;

        public DirectoryContentApplication(DirectoryContentBuilder directoryContentBuilder)
        {
            _directoryContentBuilder = directoryContentBuilder;
        }

        public DisplayableInfoDto GetDirectoryContent(string path)
        {
            log.Begin($"{nameof(GetDirectoryContent)}.{path}");
            
            IEnumerable<HashedElement> hashed = _directoryContentBuilder.GetDirectoryContent(path);
            var retVal = hashed.ToDisplayableInfoDto();
            
            log.End($"{nameof(GetDirectoryContent)}.{path}");
            return retVal;
        }
    }
}