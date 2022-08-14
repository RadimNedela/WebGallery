using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public abstract class Depot : Entity, IRacksHolder
    {
        public Depository Depository { get; private set; }

        public string Name { get; set; }

        protected abstract IEnumerable<ILocation> Locations { get; }
        private ILocation? _activeLocation;
        public ILocation ActiveLocation => _activeLocation ??= Locations.First();

        private ISet<FileSystemRootRack> _racks;
        public IReadOnlySet<FileSystemRootRack> Racks => _racks.AsReadonlySet(nameof(Racks));
        IEnumerable<RackBase> IRacksHolder.Racks => Racks;

        protected Depot(Depository depository, string hash, string name, ISet<FileSystemRootRack>? racks)
            : base(hash)
        {
            Depository = ParamAssert.NotNull(depository, nameof(depository));
            Name = ParamAssert.NotNull(name, nameof(name));
            _racks = racks ?? new HashSet<FileSystemRootRack>();
        }

        public void AddRack(RackBase rack)
        {
            ParamAssert.IsTrue(rack.Parent == this, nameof(rack));
            var rootRack = rack as FileSystemRootRack;
            ParamAssert.NotNull(rootRack, nameof(rootRack));
            _racks.Add(rootRack);
        }
    }
}
