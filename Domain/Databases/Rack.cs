using WebGalery.Domain.Contents;

namespace WebGalery.Domain.Databases
{
    public class Rack : IHashedEntity
    {
        public string Name { get; set; } = null!;

        public string Hash { get; set; } = null!;

        public IList<IRootPath> RootPaths { get; set; } = new List<IRootPath>();

        public IRootPath DefaultRootPath => RootPaths.First();

        public IList<IDirectoryBinder> DirectoryBinders { get; set; } = new List<IDirectoryBinder>();
    }
}
