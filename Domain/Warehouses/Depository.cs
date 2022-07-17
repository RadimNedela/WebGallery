namespace WebGalery.Domain.Warehouses
{
    public class Depository : Entity
    {
        public virtual string Name { get; protected set; }

        protected ISet<Depot> _depots;
        public virtual IReadOnlySet<Depot> Depots => _depots.AsReadonlySet(nameof(Depots));

        private Depot _defaultDepot;
        public Depot DefaultDepot => _defaultDepot ??= Depots.First();

        protected Depository() { }

        public Depository(string hash, string name, ISet<Depot> depots)
            : base(hash)
        {
            Name = ParamAssert.NotEmtpy(name, nameof(name));
            _depots = depots ?? new HashSet<Depot>();
        }

        public void AddDepot(Depot depot)
        {
            ParamAssert.IsTrue(depot.Depository == this, nameof(depot), "When adding new depot to depository, its parent must be the depot self");
            _depots.Add(depot);
        }
    }
}
