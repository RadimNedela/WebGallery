using WebGalery.Domain.Databases;
using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.DBModel.Factories
{
    public class DatabaseInfoDBFactory : IDatabaseInfoDBFactory
    {
        private readonly IHasher _hasher;

        public DatabaseInfoDBFactory(IHasher hasher)
        {
            _hasher = hasher;
        }

        public DatabaseInfoDB Build(Database database)
        {
            var retVal = new DatabaseInfoDB()
            {
                Hash = database.Hash,
                Name = database.Name
            };
            retVal.Racks = database.Racks.Select(r => Build(r, retVal)).ToHashSet();

            return retVal;
        }

        private RackDB Build(Rack rack, DatabaseInfoDB databaseInfoDB)
        {
            var retVal = new RackDB
            {
                Hash = rack.Hash,
                Name = rack.Name,
                DatabaseHash = databaseInfoDB.Hash,
                Database = databaseInfoDB
            };
            retVal.MountPoints = rack.RootPaths.Select(p => Build(p, retVal)).ToHashSet();

            return retVal;
        }

        private MountPointDB Build(IRootPath p, RackDB rack)
        {
            return new MountPointDB()
            {
                Hash = _hasher.ComputeDependentStringHash(rack, p.RootPath),
                Path = p.RootPath,
                RackHash = rack.Hash,
                Rack = rack
            };
        }
    }
}
