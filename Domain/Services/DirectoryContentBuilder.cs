using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;
using Domain.Elements;
using Domain.InfrastructureInterfaces;

namespace Domain.Services
{
    public class DirectoryContentBuilder
    {
        private readonly IDirectoryMethods _directoryMethods;
        private readonly HashedElementBuilder _hashedElementBuilder;

        public DirectoryContentBuilder(IDirectoryMethods directoryMethods, HashedElementBuilder hashedElementBuilder)
        {
            _directoryMethods = directoryMethods;
            _hashedElementBuilder = hashedElementBuilder;
        }

        public IEnumerable<HashedElement> GetDirectoryContent(string path)
        {
            var fileNames = _directoryMethods.GetFileNames(path);
            var dirNames = _directoryMethods.GetDirectories(path);
            var files = fileNames.Select(fn => _hashedElementBuilder.UsingFilePath(fn).Build());
            var directories = dirNames.Select(dn => _hashedElementBuilder.UsingDirectory(dn).Build());
            return directories.Union(files);
        }
    }
}