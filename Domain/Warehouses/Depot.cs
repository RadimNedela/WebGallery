using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public class Depot : Entity, IRacksHolder
    {
        public virtual Depository Depository { get; protected set; }

        public string Name { get; protected set; }

        protected ISet<ILocation> _locations;
        public virtual IReadOnlySet<ILocation> Locations => _locations.AsReadonlySet(nameof(Locations));

        private ILocation _activeLocation;
        public virtual ILocation ActiveLocation => _activeLocation ??= Locations.First();

        private ISet<Rack> _racks;
        public virtual IReadOnlySet<Rack> Racks => _racks.AsReadonlySet(nameof(Racks));

        protected Depot() { }

        public Depot(Depository depository, string hash, string name,
            ISet<ILocation> locations, ISet<Rack> racks)
            : base(hash)
        {
            Depository = ParamAssert.NotNull(depository, nameof(depository));
            Name = ParamAssert.NotNull(name, nameof(name));
            _locations = locations ?? new HashSet<ILocation>();
            _racks = racks ?? new HashSet<Rack>();
        }

        public virtual void AddRack(Rack rack)
        {
            ParamAssert.IsTrue(rack.Parent == this, nameof(rack));
            _racks.Add(rack);
        }
    }
}
