using WebGalery.Domain.Basics;

namespace WebGalery.Domain.Warehouses
{
    public class Depository : Entity
    {
        public string Name { get; set; }

        private List<FileSystemDepot> _fileSystemDepots;
        public const string FileSystemDepotsFieldName = nameof(_fileSystemDepots);
        public IEnumerable<FileSystemDepot> FileSystemDepots => _fileSystemDepots;

        private FileSystemDepot _defaultDepot;
        public FileSystemDepot DefaultDepot => _defaultDepot ??= FileSystemDepots.First();

        private Depository(string hash, string name)
            : base(hash)
        {
            Name = ParamAssert.NotEmpty(name, nameof(name));
            _fileSystemDepots = new List<FileSystemDepot>();
        }

        public Depository(string hash, string name, List<FileSystemDepot> fileSystemDepots)
            : base(hash)
        {
            Name = ParamAssert.NotEmpty(name, nameof(name));
            _fileSystemDepots = fileSystemDepots ?? new List<FileSystemDepot>();
        }

        public void AddDepot(FileSystemDepot fileSystemDepot)
        {
            ParamAssert.IsTrue(fileSystemDepot.ParentDepository == this, nameof(fileSystemDepot),
                "When adding new fileSystemDepot to depository, its parent must be this depository self");
            _fileSystemDepots.Add(fileSystemDepot);
        }
    }
}
