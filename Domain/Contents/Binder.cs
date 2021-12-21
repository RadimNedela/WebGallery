namespace WebGalery.Domain.Contents
{
    public class Binder
    {
        public IList<Binder> ChildBinders { get; set; } = new List<Binder>();

        public IList<IDisplayable> Displayables { get; set; } = new List<IDisplayable>();

        public int NumberOfDisplayables => Displayables.Count + ChildBinders.Sum(b => b.NumberOfDisplayables);
    }
}