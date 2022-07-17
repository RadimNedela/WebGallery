using WebGalery.Domain;
using WebGalery.Domain.Contents.Factories;
using WebGalery.Domain.Databases.Factories;
using WebGalery.Domain.FileServices;
using WebGalery.Domain.Warehouses;

namespace DomainTests
{
    internal class ObjectMother
    {
        private IHasher _hasher;
        public IHasher Hasher
        {
            get => _hasher ??= new Sha1Hasher();
            set => _hasher = value;
        }

        private IDirectoryReader _directoryReader;
        public IDirectoryReader DirectoryReader
        {
            get => _directoryReader ??= new DirectoryMethods();
            set => _directoryReader = value;
        }

        private IFileReader _fileReader;
        public IFileReader FileReader
        {
            get => _fileReader ??= new FileMethods();
            set => _fileReader = value;
        }

        private ILocationFactory _rootPathFactory;
        public ILocationFactory RootPathFactory
        {
            get => _rootPathFactory ??= new FileSystemRootPathFactory(DirectoryReader);
            set => _rootPathFactory = value;
        }

        private ILocation _rootPath;
        public ILocation RootPath
        {
            get => _rootPath ??= RootPathFactory.CreateDefault();
            set => _rootPath = value;
        }

        private IDepotFactory _rackFactory;
        public IDepotFactory RackFactory
        {
            get => _rackFactory ??= new RackFactory(Hasher, RootPathFactory);
            set => _rackFactory = value;
        }

        private Depot _rack;
        public Depot Rack
        {
            get => _rack ??= Database.DefaultDepot;
            set => _rack = value;
        }

        private DatabaseFactory _databaseFactory;
        public DatabaseFactory DatabaseFactory
        {
            get => _databaseFactory ??= new DatabaseFactory(Hasher, RackFactory);
            set => _databaseFactory = value;
        }

        private Depository _database;
        public Depository Database
        {
            get => _database ??= DatabaseFactory.Create(null);
            set => _database = value;
        }

        private Session _session;
        public Session Session
        {
            get => _session ??= new Session(Database, Rack, RootPath);
            set => _session = value;
        }

        private ISessionProvider _sessionProvider;
        public ISessionProvider SessionProvider
        {
            get => _sessionProvider ??= new SessionProvider(Session);
            set => _sessionProvider = value;
        }

        private DisplayableFactory _displayableFactory;
        public DisplayableFactory DisplayableFactory
        {
            get => _displayableFactory ??= new DisplayableFactory(FileReader, Hasher);
            set => _displayableFactory = value;
        }

        private DirectoryBinderFactory _directoryBinderFactory;
        public DirectoryBinderFactory DirectoryBinderFactory
        {
            get => _directoryBinderFactory ??= new DirectoryBinderFactory(DirectoryReader, DisplayableFactory, Hasher, SessionProvider);
            set => _directoryBinderFactory = value;
        }
    }
}
