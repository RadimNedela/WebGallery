namespace WebGalery.Domain.Contents
{
    public class DirectoryBinder : Binder
    {
        public DirectoryBinder(string hash, Binder parent, string name, ISet<Binder> childBinders, ISet<IDisplayable> displayables) : base(hash, parent, name, childBinders, displayables)
        {
        }

        protected DirectoryBinder()
        {
        }
    }
}