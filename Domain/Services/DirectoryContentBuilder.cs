using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Domain.Dtos;
using Domain.Elements;
using Domain.InfrastructureInterfaces;
using Domain.Logging;

namespace Domain.Services
{
    public class DirectoryContentBuilder
    {
        private readonly IDirectoryMethods _directoryMethods;
        private readonly IHasher _hasher;
        private readonly ElementsMemoryStorage _elementsMemoryStorage;

        public DirectoryContentBuilder(IDirectoryMethods directoryMethods, IHasher hasher, ElementsMemoryStorage elementsMemoryStorage)
        {
            _directoryMethods = directoryMethods;
            _hasher = hasher;
            _elementsMemoryStorage = elementsMemoryStorage;
        }

        public IList<HashedElement> GetDirectoryContent(string path)
        {
            var fileNames = _directoryMethods.GetFileNames(path);
            var dirNames = _directoryMethods.GetDirectories(path);
            BinderElement directoryBinder = CreateDirectoryBinder(path);
            var files = fileNames.Select(fn => CreateFileContentElement(fn, directoryBinder));
            var directories = dirNames.Select(dn => CreateDirectoryBinder(dn));

            return directories.Union(files).ToList();
        }

        private HashedElement CreateFileContentElement(string path, BinderElement directoryBinder)
        {
            Stream stream = _directoryMethods.GetStream(path);
            var theElement = new ContentElement()
            {
                ContentStream = stream,
                Hash = _hasher.ComputeFileContentHash(stream, path),
                Label = Path.GetFileName(path),
                Type = GetFileType(path),
                Binders = new List<BinderElement>
                {
                    directoryBinder
                }
            };

            _elementsMemoryStorage.Add(theElement);
            return theElement;
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

        private BinderElement CreateDirectoryBinder(string path)
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