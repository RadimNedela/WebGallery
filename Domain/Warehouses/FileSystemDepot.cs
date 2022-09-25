using WebGalery.Domain.Basics;
using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Warehouses
{
    public class FileSystemDepot : Entity, IRacksHolder
    {
        public Depository ParentDepository { get; private set; }

        public string Name { get; set; }

        private List<FileSystemLocation> _mountPoints;
        public const string MountPointsFieldName = nameof(_mountPoints);
        public IEnumerable<FileSystemLocation> MountPoints => _mountPoints;

        private FileSystemLocation _activeMountPoint;
        public FileSystemLocation ActiveMountPoint => _activeMountPoint ??= MountPoints.First();

        private List<FileSystemRootRack> _racks;
        public const string RacksFieldName = nameof(_racks);
        public IEnumerable<FileSystemRootRack> Racks => _racks;
        IEnumerable<RackBase> IRacksHolder.Racks => _racks;

        private FileSystemDepot(string hash, string name)
            : base(hash)
        {
            Name = name;
            _mountPoints = new List<FileSystemLocation>();
            _racks = new List<FileSystemRootRack>();
        }

        public FileSystemDepot(
            Depository depository,
            string hash,
            string name,
            List<FileSystemLocation> mountPoints,
            List<FileSystemRootRack> racks)
            : base(hash)
        {
            ParentDepository = ParamAssert.NotNull(depository, nameof(depository));
            Name = ParamAssert.NotNull(name, nameof(name));
            _mountPoints = mountPoints ?? new List<FileSystemLocation>();
            _racks = racks ?? new List<FileSystemRootRack>();
        }

        public void AddRack(FileSystemRootRack rack)
        {
            ParamAssert.NotNull(rack, nameof(rack));
            ParamAssert.IsTrue(rack.Parent == this, nameof(rack));
            _racks.Add(rack);
        }

        public void AddLocation(FileSystemLocation location)
        {
            ParamAssert.NotNull(location, nameof(location));
            ParamAssert.IsTrue(location.ParentDepot == this, nameof(location));
            _mountPoints.Add(location);
        }

        void IRacksHolder.AddRack(RackBase rack)
        {
            throw new NotImplementedException();
        }
    }
}
