using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Domain.Dtos;
using Domain.Elements;
using Domain.InfrastructureInterfaces;

namespace Domain.Services
{
    public class DirectoryContentBuilder
    {
        private readonly IDirectoryMethods _directoryMethods;
        private readonly IHasher _hasher;

        public DirectoryContentBuilder(IDirectoryMethods directoryMethods, IHasher hasher)
        {
            _directoryMethods = directoryMethods;
            _hasher = hasher;
        }

        public IEnumerable<HashedElement> GetDirectoryContent(string path)
        {
            var fileNames = _directoryMethods.GetFileNames(path);
            var dirNames = _directoryMethods.GetDirectories(path);
            var files = fileNames.Select(fn => CreateFileContentElement(fn));
            var directories = dirNames.Select(dn => CreateDirectoryBinder(dn));
            return directories.Union(files);
        }

        private HashedElement CreateFileContentElement(string path)
        {
            return new ContentElement()
            {
                Hash = _hasher.ComputeFileContentHash(path),
                Label = GetFileName(path),
                Type = GetFileType(path)
            };
        }

        private string GetFileType(string path)
        {
            string extension = System.IO.Path.GetExtension(path).ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return ContentElement.ImageType;
                default:
                    return ContentElement.UnknownType;
            }
        }

        private string GetFileName(string path)
        {
            return System.IO.Path.GetFileName(path);
        }

        private HashedElement CreateDirectoryBinder(string path)
        {
            return new BinderElement()
            {
                Hash = null,
                Binders = null,
                Contents = null,
                Label = path,
                Type = BinderElement.DirectoryType
            };
        }
    }
}