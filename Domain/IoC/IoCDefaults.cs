using WebGalery.Domain.Contents.Factories;
using WebGalery.Domain.Databases.Factories;
using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.IoC
{
    internal static class IoCDefaults
    {
        private static IHasher? hasher;
        private static IRackFactory? rackFactory;
        private static IDatabaseFactory? databaseFactory;
        private static IFileReader? fileReader;
        private static IDisplayableFactory? displayableFactory;
        private static IDirectoryReader? directoryReader;
        private static IRootPathFactory? rootPathFactory;
        private static ISessionProvider? sessionProvider;

        public static IHasher Hasher
        {
            get => hasher ??= new Sha1Hasher();
            set => hasher = value;
        }
        public static IRackFactory RackFactory
        {
            get => rackFactory ??= new RackFactory();
            set => rackFactory = value;
        }
        public static IDatabaseFactory DatabaseFactory
        {
            get => databaseFactory ??= new DatabaseFactory();
            set => databaseFactory = value;
        }
        public static IFileReader FileReader
        {
            get => fileReader ??= new FileMethods();
            set => fileReader = value;
        }
        public static IDisplayableFactory DisplayableFactory
        {
            get => displayableFactory ??= new DisplayableFactory();
            set => displayableFactory = value;
        }
        public static IDirectoryReader DirectoryReader
        {
            get => directoryReader ??= new DirectoryMethods();
            set => directoryReader = value;
        }
        public static IRootPathFactory RootPathFactory
        {
            get => rootPathFactory ??= new FileSystemRootPathFactory();
            set => rootPathFactory = value;
        }
        public static ISessionProvider SessionProvider
        {
            get => sessionProvider ??= new SessionProvider(new Session());
            set => sessionProvider = value;
        }
    }
}
