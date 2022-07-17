namespace WebGalery.Domain.Contents
{
    public class Binder : Entity
    {
        public Binder Parent { get; protected set; }

        public string Name { get; protected set; }

        protected ISet<Binder> _childBinders;
        public IReadOnlySet<Binder> ChildBinders => _childBinders.AsReadonlySet(nameof(ChildBinders));

        protected ISet<IDisplayable> _displayables;
        public IReadOnlySet<IDisplayable> Displayables => _displayables.AsReadonlySet(nameof(Displayables));

        public int NumberOfDisplayables => Displayables.Count + ChildBinders.Sum(b => b.NumberOfDisplayables);

        protected Binder() { }

        public Binder(string hash, Binder parent, string name, ISet<Binder> childBinders, ISet<IDisplayable> displayables)
            : base(hash)
        {
            Parent = parent;
            Parent?._childBinders.Add(this);
            Name = ParamAssert.NotEmtpy(name, nameof(name));
            _childBinders = childBinders ?? new HashSet<Binder>();
            _displayables = displayables ?? new HashSet<IDisplayable>();
        }

        public virtual void AddChildBinder(Binder childBinder)
        {
            ParamAssert.IsTrue(childBinder.Parent == this, nameof(childBinder), "When adding new child binder, its parent must be the binder self");
            _childBinders.Add(childBinder);
        }

        public virtual void AddOrReplaceDisplayable(string name, string hash)
        {
            var currentDisplayable = _displayables.FirstOrDefault(d => d.Name == name);
            if (currentDisplayable != null && currentDisplayable.Hash != hash)
            {
                _displayables.Remove(currentDisplayable);
                currentDisplayable = null;
            }
            if (currentDisplayable == null)
            {
                currentDisplayable = new Displayable()
                {
                    Name = name,
                    Hash = hash
                };
                _displayables.Add(currentDisplayable);
            }

        }
    }
}