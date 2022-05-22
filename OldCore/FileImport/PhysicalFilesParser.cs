using System.Collections.Generic;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Maintenance;

namespace WebGalery.Core.FileImport
{
    public class PhysicalFilesParser
    {
        private readonly IHasher _hasher;
        private readonly IActiveRackService _activeRackService;

        public PhysicalFilesParser(
            IHasher hasher,
            IActiveRackService activeRackService
            )
        {
            _hasher = hasher;
            _activeRackService = activeRackService;
        }

        public IEnumerable<PhysicalFile> ParsePhysicalFiles(DirectoryContentThreadInfo info)
        {
            foreach (var fn in info.FileNames)
            {
                PhysicalFile physicalFile = new()
                {
                    Hash = _hasher.ComputeFileContentHash(fn),
                    Type = PhysicalFile.GetContentTypeByExtension(fn),
                    FullPath = fn,
                    SubPath = _activeRackService.GetSubpath(fn)
                };

                yield return physicalFile;
            }
        }

        //private void CreateFileContentElement(string path, BinderElement directoryBinder)
        //{
        //    var hash = _hasher.ComputeFileContentHash(path);

        //    var theElement = _elementsMemoryStorage.Get(hash);

        //    if (theElement == null)
        //    {
        //        theElement = new ContentElement(hash, directoryBinder, path);
        //        _elementsMemoryStorage.Add(theElement);
        //    }
        //    else
        //    {
        //        theElement.AddLastSeenFilePosition(path, directoryBinder);
        //    }
        //}

        //private Binder CreateDirectoryBinder(string path)
        //{
        //    string subpath = pathOptimizer.CreateValidSubpathAccordingToCurrentConfiguration(path);
        //    return new Binder(pathOptimizer.Rack, hasher.ComputeStringHash(subpath), BinderTypeEnum.DirectoryType, subpath);
        //}
    }
}