using System.Collections.Generic;
using System.IO;
using WebGalery.Core.Binders;
using WebGalery.Core.DbEntities.Contents;

namespace WebGalery.Core.FileImport
{
    public class PhysicalFileToContentConverter
    {
        private readonly IBinderFactory _binderFactory;

        public PhysicalFileToContentConverter(IBinderFactory binderFactory)
        {
            _binderFactory = binderFactory;
        }

        public Content ToContentEntity(PhysicalFile physicalFile)
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

    }
}