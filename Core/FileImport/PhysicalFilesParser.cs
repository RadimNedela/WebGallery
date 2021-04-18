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
        private readonly ICurrentDatabaseInfoProvider _dbInfoProvider;
        private readonly IBinder _binder;

        public PhysicalFilesParser(
            IDirectoryMethods directoryMethods,
            IHasher hasher,
            ICurrentDatabaseInfoProvider dbInfoProvider,
            IBinder binder
            )
        {
            _directoryMethods = directoryMethods;
            _hasher = hasher;
            _dbInfoProvider = dbInfoProvider;
            _binder = binder;
        }

        public IEnumerable<ContentEntity> ParsePhysicalFiles(DirectoryContentThreadInfo info)
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

        private ContentEntity ToContentEntity(PhysicalFile physicalFile)
        {
            BinderEntity directoryBinder = _binder.GetDirectoryBinderForPath(physicalFile.FullPath);

            ContentEntity retVal = new()
            {
                Hash = physicalFile.Hash,
                Label = Path.GetFileName(physicalFile.FullPath),
                Type = physicalFile.Type.ToString(),
                AttributedBinders = new List<AttributedBinderEntityToContentEntity>()
            };

            var attToContent = new AttributedBinderEntityToContentEntity
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