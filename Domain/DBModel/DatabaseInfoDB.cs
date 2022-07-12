namespace WebGalery.Domain.DBModel
{
    public class MountPointDB : IHashedEntity
    {
        public string Hash { get; set; }
        public string Path { get; set; }
        public string RackHash { get; set; }
        public RackDB Rack { get; set; }
        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is MountPointDB mountPointObj)
                return Hash == mountPointObj.Hash;
            return false;
        }

    }

    public class RackDB : IHashedEntity
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public string DatabaseHash { get; set; }
        public DatabaseInfoDB Database { get; set; }
        public ISet<MountPointDB> MountPoints { get; set; }

        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is RackDB rackObj)
                return Hash == rackObj.Hash;
            return false;
        }
    }

    public class DatabaseInfoDB : IHashedEntity
    {
        public string Hash { get; set; }
        public string Name { get; set; }

        public ISet<RackDB> Racks { get; set; }
        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is DatabaseInfoDB databaseObj)
                return Hash == databaseObj.Hash;
            return false;
        }
    }
}
