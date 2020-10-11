using Domain.Elements;
using Domain.InfrastructureInterfaces;

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

        public BinderElement GetDirectoryContent(string path)
        {
            var fileNames = _directoryMethods.GetFileNames(path);
            var dirNames = _directoryMethods.GetDirectories(path);
            BinderElement directoryBinder = CreateDirectoryBinder(path);
            foreach (var fn in fileNames)
            {
                CreateFileContentElement(fn, directoryBinder);
            }
            foreach (var dn in dirNames)
            {
                directoryBinder.AddBinder(CreateDirectoryBinder(dn));
            }

            return directoryBinder;
        }

        private void CreateFileContentElement(string path, BinderElement directoryBinder)
        {
            var hash = _hasher.ComputeFileContentHash(path);

            var theElement = _elementsMemoryStorage.Get(hash);

            if (theElement == null)
            {
                theElement = new ContentElement(hash, directoryBinder, path);
                _elementsMemoryStorage.Add(theElement);
            }
            else
            {
                theElement.AddLastSeenFilePosition(path, directoryBinder);
            }
        }

        private BinderElement CreateDirectoryBinder(string path)
        {
            return new BinderElement(_hasher.ComputeDirectoryHash(path), BinderTypeEnum.DirectoryType, path);
        }
    }
}