using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public class Rack : Entity, IRacksHolder
    {
        public Entity Parent { get; protected set; }

        public string Name { get; protected set; }

        protected ISet<Storable> _storables;
        public IReadOnlySet<Storable> Storables => _storables.AsReadonlySet(nameof(Storables));

        public int NumberOfStorables => Storables.Count + Racks.Sum(b => b.NumberOfStorables);

        private ISet<Rack> _racks;
        public virtual IReadOnlySet<Rack> Racks => _racks.AsReadonlySet(nameof(Racks));

        protected Rack() { }

        public Rack(Entity parent, string hash, string name, ISet<Storable> storables, ISet<Rack> racks)
            : base(hash)
        {
            Parent = ParamAssert.NotNull(parent, nameof(parent));
            Name = ParamAssert.NotEmtpy(name, nameof(name));
            _storables = storables ?? new HashSet<Storable>();
            _racks = racks ?? new HashSet<Rack>();
        }

        public virtual void AddOrReplaceStorable(string hash, string name, string entityHash)
        {
            var currentStorable = _storables.FirstOrDefault(d => d.Hash == hash);
            if (currentStorable != null && currentStorable.EntityHash != entityHash)
            {
                _storables.Remove(currentStorable);
                currentStorable = null;
            }
            if (currentStorable == null)
            {
                currentStorable = new Storable(hash, name, entityHash);
                _storables.Add(currentStorable);
            }
        }

        public virtual void AddRack(Rack rack)
        {
            ParamAssert.IsTrue(rack.Parent == this, nameof(rack));
            _racks.Add(rack);
        }
    }
}