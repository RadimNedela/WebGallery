namespace Domain.DbEntities.Maintenance
{
    public class MountPointEntity : HashedEntity
    {
        public string Path { get; set; }
        public RackEntity Rack { get; set; }
        public string RackId { get; set; }
    }
}
