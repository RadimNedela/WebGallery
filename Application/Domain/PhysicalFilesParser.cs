using System.Collections.Generic;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Maintenance.Domain;

namespace WebGalery.FileImport.Domain
{
    public class PhysicalFilesParser
    {
        private readonly IDirectoryMethods directoryMethods;
        private readonly IHasher hasher;
        private readonly CurrentDatabaseInfoProvider dbInfoProvider;

        public PhysicalFilesParser(
            IDirectoryMethods directoryMethods,
            IHasher hasher,
            CurrentDatabaseInfoProvider dbInfoProvider
            )
        {
            this.directoryMethods = directoryMethods;
            this.hasher = hasher;
            this.dbInfoProvider = dbInfoProvider;
        }

        public IEnumerable<PhysicalFile> ParsePhysicalFiles(DirectoryContentThreadInfo info)
        {
            var rack = dbInfoProvider.CurrentInfo.CurrentRack;
            info.FileNames = directoryMethods.GetFileNames(info.FullPath);
            foreach (var fn in info.FileNames)
            {
                info.FilesDone++;

                PhysicalFile physicalFile = new()
                {
                    Hash = hasher.ComputeFileContentHash(fn),
                    Type = PhysicalFile.GetContentTypeByExtension(fn),
                    FullPath = fn,
                    SubPath = rack.GetSubpath(fn)
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