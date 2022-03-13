using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Databases.Factories
{
    internal class RackFactory : IRackFactory
    {
        private readonly IHasher hasher;
        private readonly IRootPathFactory rootPathFactory;

        public RackFactory(IHasher hasher, IRootPathFactory rootPathFactory)
        {
            this.hasher = hasher;
            this.rootPathFactory = rootPathFactory;
        }

        public Rack CreateDefaultFor(Database parent)
        {
            var rack = new Rack();
            var rootPath = rootPathFactory.Create();
            rack.RootPaths.Add(rootPath);
            rack.Name = "Rack " + rootPath.RootPath + " " + DateTime.Now.ToString("G");
            rack.Hash = hasher.ComputeDependentStringHash(parent, rack.Name);
            return rack;
        }
    }
}
