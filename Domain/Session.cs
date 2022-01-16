using WebGalery.Domain.Databases;
using WebGalery.Domain.IoC;

namespace WebGalery.Domain
{
    public class Session
    {
        private Database? activeDatabase;
        private Rack? activeRack;
        private IRootPath? activeRootPath;

        public Database ActiveDatabase
        {
            get => activeDatabase ??= IoCDefaults.DatabaseFactory.Create();
            set => activeDatabase = value;
        }

        public Rack ActiveRack
        {
            get => activeRack ??= ActiveDatabase.DefaultRack;
            set => activeRack = value;
        }

        public IRootPath ActiveRootPath
        {
            get => activeRootPath ??= ActiveRack.DefaultRootPath;
            set => activeRootPath = value;
        }
    }
}
