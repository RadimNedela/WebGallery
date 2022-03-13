namespace WebGalery.Domain.Contents
{
    public class Binder : IHashedEntity
    {
        public Binder? Parent { get; set; }

        public IList<Binder> ChildBinders { get; set; } = new List<Binder>();

        public IList<IDisplayable> Displayables { get; set; } = new List<IDisplayable>();

        public int NumberOfDisplayables => Displayables.Count + ChildBinders.Sum(b => b.NumberOfDisplayables);

        public string Name { get; internal set; } = null!; // null-forgiving operator (!)

        public string Hash { get; internal set; } = null!;

        public Binder Initialize(Binder parent, string name, string hash)
        {
            Parent = parent;
            Parent.ChildBinders.Add(this);
            Name = name;
            Hash = hash;

            return this;
        }
    }
}