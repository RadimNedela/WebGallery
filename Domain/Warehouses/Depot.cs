using WebGalery.Domain.Contents;

namespace WebGalery.Domain.Warehouses
{
    public class Depot : Binder
    {
        public virtual Depository Depository { get; protected set; }

        protected ISet<ILocation> _locations;
        public virtual IReadOnlySet<ILocation> Locations => _locations.AsReadonlySet(nameof(Locations));

        private ILocation _activeLocation;
        public virtual ILocation ActiveLocation => _activeLocation ??= Locations.First();

        public virtual IEnumerable<Rack> Racks =>
            ChildBinders
            .Where(db => typeof(Rack).IsAssignableFrom(db.GetType()))
            .Select(db => (Rack)db);

        protected Depot() { }

        public Depot(Depository depository, string hash, string name,
            ISet<Binder> childBinders, ISet<IDisplayable> displayables, ISet<ILocation> locations)
            : base(hash, null, name, childBinders, displayables)
        {
            Depository = ParamAssert.NotNull(depository, nameof(depository));
            _locations = locations ?? new HashSet<ILocation>();
        }
    }
}
