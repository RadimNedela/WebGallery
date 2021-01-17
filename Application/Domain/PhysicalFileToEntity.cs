using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.FileImport.Domain
{
    public class PhysicalFileToEntity
    {
        private readonly IContentEntityRepository contentRepository;

        public PhysicalFileToEntity(IContentEntityRepository contentRepository)
        {
            this.contentRepository = contentRepository;
        }

        public ContentEntity ToContentEntity(PhysicalFile physicalFile)
        {
            return null;
        }
    }
}
