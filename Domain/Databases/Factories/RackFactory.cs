using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Databases.Factories
{
    internal class RackFactory : IRackFactory
    {
        private readonly IHasher hasher;

        public RackFactory(IHasher hasher)
        {
            this.hasher = hasher;
        }

        public Rack CreateFor(IHashedEntity parent)
        {
            var rack = new Rack();
            var rootPath = new FileSystemRootPath(new DirectoryMethods());
            rack.RootPaths.Add(rootPath);
            rack.Name = "Rack " + rootPath.RootPath + " " + DateTime.Now.ToString("G");
            rack.Hash = hasher.ComputeDependentStringHash(parent, rack.Name);
            return rack;
        }
    }
}
