using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public class FileSystemRack : RackBase
    {
        public RackBase ParentRack { get; private set; }

        public override Entity Parent => ParentRack;

        private FileSystemRack(string hash, string name)
            : base(hash, name) { }

        public FileSystemRack(
            RackBase parentRack,
            string hash,
            string name,
            List<Storable> storables,
            List<RackBase> racks)
            : base(hash, name, storables, racks)
        {
            ParentRack = ParamAssert.NotNull(parentRack, nameof(parentRack));
        }
    }
}