using WebGallery.DatabaseEntities;

namespace Domain.DbEntities.Maintenance
{
    public class MountPointEntity : IDatabaseEntity
    {
        public string Path { get; set; }
        public RackEntity Rack { get; set; }
        public string RackId { get; set; }
    }
}
