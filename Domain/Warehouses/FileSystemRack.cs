using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public class FileSystemRack : RackBase
    {
        public RackBase ParentRack { get; private set; }

        public override Entity Parent => ParentRack;

        public FileSystemRack(
            RackBase parentRack,
            string hash,
            string name,
            ISet<Storable> storables,
            ISet<RackBase> racks)
            : base(hash, name, storables, racks)
        {
            ParentRack = parentRack;
        }

    }
}