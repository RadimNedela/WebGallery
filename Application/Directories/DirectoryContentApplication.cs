using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;
using Domain.Elements;
using Domain.Services;

namespace Application.Directories
{
    public class DirectoryContentApplication
    {
        private readonly DirectoryContentBuilder _directoryContentBuilder;

        public DirectoryContentApplication(DirectoryContentBuilder directoryContentBuilder)
        {
            _directoryContentBuilder = directoryContentBuilder;
        }

        public DisplayableInfoDto GetDirectoryContent(string path)
        {
            IEnumerable<HashedElement> hashed = _directoryContentBuilder.GetDirectoryContent(path);
            return hashed.ToDisplayableInfoDto();
        }
    }
}