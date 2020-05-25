using System.Collections.Generic;
using Domain.Dtos;
using Domain.Services;

namespace Application.Directories
{
    public class DirectoryContentApplication : IDirectoryContentApplication
    {
        private readonly DirectoryContentBuilder _directoryContentBuilder;

        public DirectoryContentApplication(DirectoryContentBuilder directoryContentBuilder)
        {
            _directoryContentBuilder = directoryContentBuilder;
        }

        public IEnumerable<DirectoryElementDto> GetDirectoryContent(string path)
        {
            return _directoryContentBuilder.GetDirectoryContent(path);
        }
    }
}