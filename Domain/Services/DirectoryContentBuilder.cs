using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;
using Domain.InfrastructureInterfaces;

namespace Domain.Services
{
    public class DirectoryContentBuilder
    {
        private readonly IDirectoryMethods _directoryMethods;
        private readonly FileInfoBuilder _fileInfoBuilder;

        public DirectoryContentBuilder(IDirectoryMethods directoryMethods, FileInfoBuilder fileInfoBuilder)
        {
            _directoryMethods = directoryMethods;
            _fileInfoBuilder = fileInfoBuilder;
        }

        public IEnumerable<DirectoryElementDto> GetDirectoryContent(string path)
        {
            var fileNames = _directoryMethods.GetFileNames(path);
            var dirNames = _directoryMethods.GetDirectories(path);
            var files = fileNames.Select(fn => _fileInfoBuilder.UsingPath(fn).Build().GetFileInfoDto());
            IEnumerable<DirectoryElementDto> directories = dirNames.Select(dn => new DirectoryInfoDto() { FileName = dn });
            return directories.Union(files);
        }
    }
}