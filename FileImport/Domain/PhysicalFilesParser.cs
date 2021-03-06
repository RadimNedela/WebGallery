using System.Collections.Generic;
using System.IO;
using WebGalery.Core.BinderInterfaces;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.FileImport.Domain
{
    public class PhysicalFilesParser
    {
        private readonly IDirectoryMethods directoryMethods;
        private readonly IHasher hasher;
        private readonly ICurrentDatabaseInfoProvider dbInfoProvider;
        private readonly IContentEntityRepository contentRepository;
        private readonly IBinder binder;

        public PhysicalFilesParser(
            IDirectoryMethods directoryMethods,
            IHasher hasher,
            ICurrentDatabaseInfoProvider dbInfoProvider,
            IContentEntityRepository contentRepository,
            IBinder binder
            )
        {
            this.directoryMethods = directoryMethods;
            this.hasher = hasher;
            this.dbInfoProvider = dbInfoProvider;
            this.contentRepository = contentRepository;
            this.binder = binder;
        }

        public IEnumerable<ContentEntity> ParsePhysicalFiles(DirectoryContentThreadInfo info)
        {
            var rack = dbInfoProvider.CurrentInfo.ActiveRack;
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

                yield return ToContentEntity(physicalFile);
            }
        }

        private ContentEntity ToContentEntity(PhysicalFile physicalFile)
        {
            BinderEntity directoryBinder = binder.GetDirectoryBinderForPath(physicalFile.FullPath);

            ContentEntity retVal = new()
            {
                Hash = physicalFile.Hash,
                Label = Path.GetFileName(physicalFile.FullPath),
                Type = physicalFile.Type.ToString(),
                AttributedBinders = new List<AttributedBinderEntityToContentEntity>()
            };

            retVal.AttributedBinders.Add(new AttributedBinderEntityToContentEntity
            {
                Attribute = physicalFile.SubPath,
                Binder = directoryBinder,
                BinderHash = directoryBinder.Hash,
                Content = retVal,
                ContentHash = retVal.Hash
            });

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