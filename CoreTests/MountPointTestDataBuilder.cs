using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Core.Tests
{
    public class MountPointTestDataBuilder
    {
        public string Path { get; private set; }
        public RackEntity Rack { get; private set; }
        public string RackHash { get; private set; }

        public static MountPointTestDataBuilder CreateWindowsDefault()
        {
            return new MountPointTestDataBuilder()
                .WithPath(@"C:\temp");
        }

        public static MountPointTestDataBuilder CreateLinuxDefault()
        {
            return new MountPointTestDataBuilder()
                .WithPath(@"/tmp");
        }

        public MountPointTestDataBuilder WithPath(string path)
        {
            Path = path;
            return this;
        }

        internal MountPointTestDataBuilder Using(RackEntity rack)
        {
            Rack = rack;
            RackHash = rack.Hash;
            return this;
        }

        public MountPointEntity Build()
        {
            return new MountPointEntity
            {
                Path = Path,
                Rack = Rack,
                RackHash = RackHash
            };
        }
    }
}