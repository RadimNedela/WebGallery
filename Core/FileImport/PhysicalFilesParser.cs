using System.Collections.Generic;
using System.IO;
using WebGalery.Core.Binders;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Maintenance;

namespace WebGalery.Core.FileImport
{
    public class PhysicalFilesParser
    {
        private readonly IHasher _hasher;
        private readonly IActiveRackService _activeRackService;
        private readonly IBinderFactory _binderFactory;

        public PhysicalFilesParser(
            IHasher hasher,
            IActiveRackService activeRackService,
            IBinderFactory binderFactory
            )
        {
            _hasher = hasher;
            _activeRackService = activeRackService;
            _binderFactory = binderFactory;
        }

        public IEnumerable<Content> ParsePhysicalFiles(DirectoryContentThreadInfo info)
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

                yield return ToContentEntity(physicalFile);
            }
        }

        private Content ToContentEntity(PhysicalFile physicalFile)
        {
            Binder directoryBinder = _binderFactory.GetOrBuildDirectoryBinderForPath(physicalFile.FullPath);

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