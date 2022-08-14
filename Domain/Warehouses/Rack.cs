using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public abstract class RackBase : Entity, IRacksHolder
    {
        public abstract Entity Parent { get; }

        public string Name { get; set; }

        protected ISet<Storable> _storables;
        public IReadOnlySet<Storable> Storables => _storables.AsReadonlySet(nameof(Storables));

        public int NumberOfStorables => Storables.Count + Racks.Sum(b => b.NumberOfStorables);

        private ISet<RackBase> _racks;
        public virtual IEnumerable<RackBase> Racks => _racks.AsReadonlySet(nameof(Racks));

        protected RackBase(string hash, string name, ISet<Storable> storables, ISet<RackBase> racks)
            : base(hash)
        {
            Name = ParamAssert.NotEmtpy(name, nameof(name));
            _storables = storables ?? new HashSet<Storable>();
            _racks = racks ?? new HashSet<RackBase>();
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

        public virtual void AddRack(RackBase rack)
        {
            ParamAssert.IsTrue(rack.Parent == this, nameof(rack));
            _racks.Add(rack);
        }
    }
}