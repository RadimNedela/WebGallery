using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Contents
{
    public class Binder : Entity
    {
        public Binder? Parent { get; private set; }

        public string Name { get; set; }

        protected ISet<Binder> _childBinders;
        public IReadOnlySet<Binder> ChildBinders => _childBinders.AsReadonlySet(nameof(ChildBinders));

        public Binder(string hash, Binder? parent, string name, ISet<Binder>? childBinders)
            : base(hash)
        {
            Parent = parent;
            Parent?._childBinders.Add(this);
            Name = ParamAssert.NotEmtpy(name, nameof(name));
            _childBinders = childBinders ?? new HashSet<Binder>();
        }

        public virtual void AddChildBinder(Binder childBinder)
        {
            ParamAssert.IsTrue(childBinder.Parent == this, nameof(childBinder), "When adding new child binder, its parent must be the binder self");
            _childBinders.Add(childBinder);
        }
    }
}