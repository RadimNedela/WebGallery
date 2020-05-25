using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;
using Domain.InfrastructureInterfaces;

namespace Domain.Services
{
    public class DirectoryContentBuilder
    {
        private readonly IDirectoryMethods _directoryMethods;

        public DirectoryContentBuilder(IDirectoryMethods directoryMethods)
        {
            _directoryMethods = directoryMethods;
        }

        public IEnumerable<DirectoryElementDto> GetDirectoryContent(string path)
        {
            var fileNames = _directoryMethods.GetFileNames(path);
            var dirNames = _directoryMethods.GetDirectories(path);
            var files = fileNames.Select(fn => new FileInfoDto() { FileName = fn });
            var directories = dirNames.Select(dn => new DirectoryInfoDto() { FileName = dn });

            return ((IEnumerable<DirectoryElementDto>)directories).Union(files);
        }
    }
}