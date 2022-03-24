namespace WebGalery.Domain.DBModel
{
    public class DBEntity
    {
        public string? Hash { get; set; }
    }
    public class MountPointDB : DBEntity
    {
        public string? Path { get; set; }
        public string? RackHash { get; set; }
        public RackDB? Rack { get; set; }
    }

    public class RackDB : DBEntity
    {
        public string? Name { get; set; }
        public string? DatabaseHash { get; set; }
        public DatabaseInfoDB? Database { get; set; }
        public IList<MountPointDB>? MountPoints { get; set; }
    }

    public class DatabaseInfoDB : DBEntity
    {
        public string? Name { get; set; }

        public IList<RackDB>? Racks { get; set; }
    }
}
