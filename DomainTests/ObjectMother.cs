using WebGalery.Domain;
using WebGalery.Domain.Contents.Factories;
using WebGalery.Domain.Databases;
using WebGalery.Domain.Databases.Factories;
using WebGalery.Domain.FileServices;

namespace DomainTests
{
    internal class ObjectMother
    {
        private IHasher? _hasher;
        public IHasher Hasher
        {
            get => _hasher ??= new Sha1Hasher();
            set => _hasher = value;
        }

        private IDirectoryReader? _directoryReader;
        public IDirectoryReader DirectoryReader
        {
            get => _directoryReader ??= new DirectoryMethods();
            set => _directoryReader = value;
        }

        private IFileReader? _fileReader;
        public IFileReader FileReader
        {
            get => _fileReader ??= new FileMethods();
            set => _fileReader = value;
        }

        private IRootPathFactory? _rootPathFactory;
        public IRootPathFactory RootPathFactory
        {
            get => _rootPathFactory ??= new FileSystemRootPathFactory(DirectoryReader);
            set => _rootPathFactory = value;
        }

        private IRootPath? _rootPath;
        public IRootPath RootPath
        {
            get => _rootPath ??= RootPathFactory.Create();
            set => _rootPath = value;
        }

        private IRackFactory? _rackFactory;
        public IRackFactory RackFactory
        {
            get => _rackFactory ??= new RackFactory(Hasher, RootPathFactory);
            set => _rackFactory = value;
        }

        private Rack? _rack;
        public Rack Rack
        {
            get => _rack ??= Database.DefaultRack;
            set => _rack = value;
        }

        private DatabaseFactory? _databaseFactory;
        public DatabaseFactory DatabaseFactory
        {
            get => _databaseFactory ??= new DatabaseFactory(Hasher, RackFactory);
            set => _databaseFactory = value;
        }

        private Database? _database;
        public Database Database
        {
            get => _database ??= DatabaseFactory.Create();
            set => _database = value;
        }

        private Session? _session;
        public Session Session
        {
            get => _session ??= new Session(Database, Rack, RootPath);
            set => _session = value;
        }

        private ISessionProvider? _sessionProvider;
        public ISessionProvider SessionProvider
        {
            get => _sessionProvider ??= new SessionProvider(Session);
            set => _sessionProvider = value;
        }

        private IDisplayableFactory? _displayableFactory;
        public IDisplayableFactory DisplayableFactory
        {
            get => _displayableFactory ??= new DisplayableFactory(FileReader, Hasher);
            set => _displayableFactory = value;
        }

        private DirectoryBinderFactory? _directoryBinderFactory;
        public DirectoryBinderFactory DirectoryBinderFactory
        {
            get => _directoryBinderFactory ??= new DirectoryBinderFactory(DirectoryReader, DisplayableFactory, Hasher, SessionProvider);
            set => _directoryBinderFactory = value;
        }
    }
}
