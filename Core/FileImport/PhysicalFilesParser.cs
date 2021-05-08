using System.Collections.Generic;
using System.IO;
using WebGalery.Core.BinderInterfaces;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.FileImport
{
    public class PhysicalFilesParser
    {
        private readonly IDirectoryMethods _directoryMethods;
        private readonly IHasher _hasher;
        private readonly IActiveDatabaseInfoProvider _dbInfoProvider;
        private readonly IBinder _binder;

        public PhysicalFilesParser(
            IDirectoryMethods directoryMethods,
            IHasher hasher,
            IActiveDatabaseInfoProvider dbInfoProvider,
            IBinder binder
            )
        {
            _directoryMethods = directoryMethods;
            _hasher = hasher;
            _dbInfoProvider = dbInfoProvider;
            _binder = binder;
        }

        public IEnumerable<Content> ParsePhysicalFiles(DirectoryContentThreadInfo info)
        {
            var rack = _dbInfoProvider.CurrentInfo.ActiveRack;
            info.FileNames = _directoryMethods.GetFileNames(info.FullPath);
            foreach (var fn in info.FileNames)
            {
                info.FilesDone++;

                PhysicalFile physicalFile = new()
                {
                    Hash = _hasher.ComputeFileContentHash(fn),
                    Type = PhysicalFile.GetContentTypeByExtension(fn),
                    FullPath = fn,
                    SubPath = rack.GetSubpath(fn)
                };

                yield return ToContentEntity(physicalFile);
            }
        }

        private Content ToContentEntity(PhysicalFile physicalFile)
        {
            Binder directoryBinder = _binder.GetDirectoryBinderForPath(physicalFile.FullPath);

            Content retVal = new()
            {
                Hash = physicalFile.Hash,
                Label = Path.GetFileName(physicalFile.FullPath),
                Type = physicalFile.Type.ToString(),
                AttributedBinders = new List<AttributedBinderToContent>()
            };

            var attToContent = new AttributedBinderToContent
            {
                Attribute = physicalFile.SubPath,
                Binder = directoryBinder,
                BinderHash = directoryBinder.Hash,
                Content = retVal,
                ContentHash = retVal.Hash
            };

            retVal.AttributedBinders.Add(attToContent);
            directoryBinder.AttributedContents.Add(attToContent);
            
            return retVal;
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