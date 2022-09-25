using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public abstract class RackBase : Entity, IRacksHolder
    {
        public abstract Entity Parent { get; }

        public string Name { get; set; }

        protected List<Storable> _storables;
        public IEnumerable<Storable> Storables => _storables;

        public int NumberOfStorables => Storables.Count() + Racks.Sum(b => b.NumberOfStorables);

        private List<RackBase> _racks;
        public IEnumerable<RackBase> Racks => _racks;

        protected RackBase(string hash, string name)
        : base(hash)
        {
            Name = name;
        }
        protected RackBase(string hash, string name, List<Storable> storables, List<RackBase> racks)
        : base(hash)
        {
            Name = ParamAssert.NotEmpty(name, nameof(name));
            _storables = storables ?? new List<Storable>();
            _racks = racks ?? new List<RackBase>();
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