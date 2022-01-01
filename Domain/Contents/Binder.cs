﻿namespace WebGalery.Domain.Contents
{
    public class Binder : IHashedEntity
    {
        public IList<Binder> ChildBinders { get; set; } = new List<Binder>();

        public IList<IDisplayable> Displayables { get; set; } = new List<IDisplayable>();

        public int NumberOfDisplayables => Displayables.Count + ChildBinders.Sum(b => b.NumberOfDisplayables);

        public string Name { get; internal set; } = null!; // null-forgiving operator (!)

        public string Hash { get; internal set; } = null!;
    }
}