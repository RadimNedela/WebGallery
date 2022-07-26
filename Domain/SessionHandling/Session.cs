using WebGalery.Domain.Warehouses;

namespace WebGalery.Domain.SessionHandling
{
    public class Session
    {
        public Depository ActiveDepository { get; set; }

        public Depot ActiveDepot { get; set; }

        public ILocation ActiveLocation { get; set; }

        public Session(Depository activeDepository, Depot activeDepot, ILocation activeLocation)
        {
            ActiveDepository = activeDepository;
            ActiveDepot = activeDepot;
            ActiveLocation = activeLocation;
        }
    }
}
