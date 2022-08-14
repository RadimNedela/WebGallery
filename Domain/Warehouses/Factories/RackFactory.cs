using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses.Factories
{
    public class RackFactory
    {
        private readonly IHasher _hasher;

        public RackFactory(IHasher hasher)
        {
            _hasher = hasher;
        }

        public RackBase BuildFileSystemRack(Entity parentEntity, string name)
        {
            string hash = _hasher.ComputeDependentStringHash(parentEntity, name);

            RackBase newRack;
            if (parentEntity is Depot parentDepot)
            {
                newRack = new FileSystemRootRack(parentDepot, hash, name, null, null);
            }
            else if (parentEntity is RackBase parentRack)
            {
                newRack = new FileSystemRack(parentRack, hash, name, null, null);
            }
            else
            {
                throw new NotSupportedException($"The type {parentEntity.GetType} is not supported");
            }

            return newRack;
        }
    }
}
