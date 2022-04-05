using WebGalery.Domain.Contents;

namespace WebGalery.Domain.Databases
{
    public class Rack : Binder
    {
        public IList<IRootPath> RootPaths { get; set; } = new List<IRootPath>();

        private IRootPath _activeRootPath;
        public IRootPath ActiveRootPath
        {
            get => _activeRootPath ??= RootPaths.First();
            set => _activeRootPath = value;
        }

        public IList<DirectoryBinder> DirectoryBinders { get; set; } = new List<DirectoryBinder>();
    }
}
