using WebGalery.Domain.Databases.Factories;
using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.IoC
{
    internal static class IoCDefaults
    {
        public static IHasher Hasher { get; } = new Sha1Hasher();
        public static IRackFactory RackFactory { get; } = new RackFactory();
        public static DatabaseFactory DatabaseFactory { get; } = new DatabaseFactory();
    }
}
