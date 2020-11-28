using WebGallery.DatabaseEntities;

namespace Domain.DbEntities
{
    public class BinderEntityToContentEntity : IDatabaseEntity
    {
        public int BinderId { get; set; }
        public BinderEntity Binder { get; set; }

        public int ContentId { get; set; }
        public ContentEntity Content { get; set; }
    }
}