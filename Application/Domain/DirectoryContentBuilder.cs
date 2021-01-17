using System.Collections.Generic;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.FileImport.Domain.Elements;

namespace WebGalery.FileImport.Domain
{
    public class DirectoryContentBuilder : IDirectoryContentBuilder
    {
        private readonly IDirectoryMethods directoryMethods;
        private readonly IHasher hasher;
        //private readonly IContentElementsMemoryStorage elementsMemoryStorage;

        public DirectoryContentBuilder(
            IDirectoryMethods directoryMethods,
            IHasher hasher
            //IContentElementsMemoryStorage elementsMemoryStorage,
            )
        {
            this.directoryMethods = directoryMethods;
            this.hasher = hasher;
            //this.elementsMemoryStorage = elementsMemoryStorage;
        }

        public IEnumerable<PhysicalFile> ParsePhysicalFiles(DirectoryContentThreadInfo info)
        {
            info.FileNames = directoryMethods.GetFileNames(info.FullPath);
            //Binder directoryBinder = CreateDirectoryBinder(info.FullPath);
            foreach (var fn in info.FileNames)
            {
                //CreateFileContentElement(fn, directoryBinder);
                info.FilesDone++;

                ContentEntity contentEntity = new ContentEntity()
                {
                    //Hash = hasher.ComputeFileContentHash(fn),
                };

                yield return new PhysicalFile { ContentEntity = contentEntity };
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