using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public class FileSystemRootRack : RackBase
    {
        public FileSystemDepot ParentDepot { get; private set; }

        public override Entity Parent => ParentDepot;

        private FileSystemRootRack(string hash, string name)
            : base(hash, name) { }

        public FileSystemRootRack(
            FileSystemDepot parentDepot,
            string hash,
            string name,
            List<Storable> storables,
            List<RackBase> racks)
            : base(hash, name, storables, racks)
        {
            ParentDepot = ParamAssert.NotNull(parentDepot, nameof(parentDepot));
        }
    }
}