namespace WebGalery.Core.Maintenance
{
    public class MountPoint : IPersistable
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public Rack Rack { get; set; }
        public string RackHash { get; set; }
    }
}
