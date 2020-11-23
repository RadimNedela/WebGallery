using Domain.Elements;
using Domain.Services.InfrastructureInterfaces;

namespace Domain.Services
{
    public class DirectoryContentBuilder
    {
        private readonly IDirectoryMethods _directoryMethods;
        private readonly IHasher _hasher;
        private readonly IContentElementRepository _elementsMemoryStorage;
        private readonly IPathOptimizer _pathOptimizer;

        public DirectoryContentBuilder(
            IDirectoryMethods directoryMethods,
            IHasher hasher,
            IContentElementRepository elementsMemoryStorage,
            IPathOptimizer pathOptimizer)
        {
            _directoryMethods = directoryMethods;
            _hasher = hasher;
            _elementsMemoryStorage = elementsMemoryStorage;
            _pathOptimizer = pathOptimizer;
        }

        public BinderElement GetDirectoryContent(DirectoryContentThreadInfo info)
        {
            info.FileNames = _directoryMethods.GetFileNames(info.FullPath);
            info.DirNames = _directoryMethods.GetDirectories(info.FullPath);
            BinderElement directoryBinder = CreateDirectoryBinder(info.FullPath);
            foreach (var fn in info.FileNames)
            {
                CreateFileContentElement(fn, directoryBinder);
                info.FilesDone++;
            }
            foreach (var dn in info.DirNames)
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
            string subpath = _pathOptimizer.CreateValidSubpathAccordingToCurrentConfiguration(path);
            return new BinderElement(_pathOptimizer.Rack, _hasher.ComputeStringHash(subpath), BinderTypeEnum.DirectoryType, subpath);
        }
    }
}