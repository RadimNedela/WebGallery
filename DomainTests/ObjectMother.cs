using WebGalery.Domain.Basics;
using WebGalery.Domain.FileServices;
using WebGalery.Domain.SessionHandling;
using WebGalery.Domain.Warehouses;
using WebGalery.Domain.Warehouses.Factories;
using WebGalery.Domain.Warehouses.Loaders;

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

        private ILocationFactory _locationFactory;
        public ILocationFactory LocationFactory
        {
            get => _locationFactory ??= new FileSystemLocationFactory(DirectoryReader);
            set => _locationFactory = value;
        }

        private ILocation _rootPath;
        public ILocation RootPath
        {
            get => _rootPath ??= LocationFactory.CreateDefault();
            set => _rootPath = value;
        }

        private IDepotFactory _depotFactory;
        public IDepotFactory DepotFactory
        {
            get => _depotFactory ??= new FileSystemDepotFactory(Hasher, LocationFactory);
            set => _depotFactory = value;
        }

        private Depot _rack;
        public Depot Rack
        {
            get => _rack ??= Depository.DefaultDepot;
            set => _rack = value;
        }

        private DepositoryFactory _depositoryFactory;
        public DepositoryFactory DepositoryFactory
        {
            get => _depositoryFactory ??= new DepositoryFactory(Hasher, DepotFactory);
            set => _depositoryFactory = value;
        }

        private Depository _depository;
        public Depository Depository
        {
            get => _depository ??= DepositoryFactory.Build(null);
            set => _depository = value;
        }

        private Session _session;
        public Session Session
        {
            get => _session ??= new Session(Depository, Rack, RootPath);
            set => _session = value;
        }

        private ISessionProvider _sessionProvider;
        public ISessionProvider SessionProvider
        {
            get => _sessionProvider ??= new SessionProvider(Session);
            set => _sessionProvider = value;
        }

        //private DisplayableFactory _displayableFactory;
        //public DisplayableFactory DisplayableFactory
        //{
        //    get => _displayableFactory ??= new DisplayableFactory(FileReader, Hasher);
        //    set => _displayableFactory = value;
        //}

        private FileSystemDirectoryLoader _fileSystemDirectoryLoader;
        public FileSystemDirectoryLoader FileSystemDirectoryLoader
        {
            get => _fileSystemDirectoryLoader ??= new FileSystemDirectoryLoader(DirectoryReader, Hasher, SessionProvider, FileReader);
            set => _fileSystemDirectoryLoader = value;
        }
    }
}
