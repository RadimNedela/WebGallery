using WebGallery.DatabaseEntities;

namespace Domain.DbEntities
{
    public class BinderEntityToContentEntity : IDatabaseEntity
    {
        public string BinderHash { get; set; }
        public BinderEntity Binder { get; set; }

        public string ContentHash { get; set; }
        public ContentEntity Content { get; set; }
    }
}