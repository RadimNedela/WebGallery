using System.Collections.Generic;
using System.IO;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.FileImport.Domain
{
    public class PhysicalFileToEntity
    {
        private readonly IContentEntityRepository contentRepository;
        private readonly Binder binder;

        public PhysicalFileToEntity(
            IContentEntityRepository contentRepository,
            Binder binder
            )
        {
            this.contentRepository = contentRepository;
            this.binder = binder;
        }

        public ContentEntity ToContentEntity(PhysicalFile physicalFile)
        {
            BinderEntity directoryBinder = binder.GetDirectoryBinderForPhysicalFile(physicalFile);

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
                Content = retVal
            });

            return retVal;
        }
    }
}
