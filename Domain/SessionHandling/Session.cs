using WebGalery.Domain.FileServices;
using WebGalery.Domain.Warehouses;

namespace WebGalery.Domain.SessionHandling
{
    public class Session
    {
        public Depository ActiveDepository { get; set; }

        public FileSystemDepot ActiveDepot { get; set; }

        public FileSystemLocation ActiveLocation { get; set; }

        public Session(Depository activeDepository, FileSystemDepot activeDepot, FileSystemLocation activeLocation)
        {
            ActiveDepository = activeDepository;
            ActiveDepot = activeDepot;
            ActiveLocation = activeLocation;
        }
    }
}
