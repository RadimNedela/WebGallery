using System.Collections.ObjectModel;
using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public class Depository : Entity
    {
        public virtual string Name { get; set; }

        private readonly List<Depot> _depots;
        public const string DepotsFieldName = nameof(_depots);
        public virtual ReadOnlyCollection<Depot> Depots => _depots.AsReadOnly();

        private Depot _defaultDepot;
        public Depot DefaultDepot => _defaultDepot ??= Depots.First();

        private Depository(string hash, string name)
            : base(hash)
        {
            Name = ParamAssert.NotEmtpy(name, nameof(name));
            _depots = new List<Depot>();
        }

        public Depository(string hash, string name, List<Depot> depots)
            : base(hash)
        {
            Name = ParamAssert.NotEmtpy(name, nameof(name));
            _depots = depots ?? new List<Depot>();
        }

        public void AddDepot(Depot depot)
        {
            ParamAssert.IsTrue(depot.Depository == this, nameof(depot), "When adding new depot to depository, its parent must be the depot self");
            _depots.Add(depot);
        }
    }
}
