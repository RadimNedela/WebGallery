using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public class FileSystemRootRack : RackBase
    {
        public Depot ParentDepot { get; private set; }

        public override Entity Parent => ParentDepot;

        public FileSystemRootRack(
            Depot parentDepot,
            string hash,
            string name,
            ISet<Storable>? storables,
            ISet<RackBase>? racks)
            : base(hash, name, storables, racks)
        {
            ParentDepot = parentDepot;
        }
    }
}