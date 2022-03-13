using WebGalery.Domain.Databases;

namespace WebGalery.Domain
{
    public class Session
    {
        public Database ActiveDatabase { get; set; }

        public Rack ActiveRack { get; set; }

        public IRootPath ActiveRootPath { get; set; }

        public Session(Database database, Rack rack, IRootPath rootPath)
        {
            ActiveDatabase = database;
            ActiveRack = rack;
            ActiveRootPath = rootPath;
        }
    }
}
