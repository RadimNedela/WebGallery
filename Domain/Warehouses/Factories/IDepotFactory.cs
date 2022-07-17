using WebGalery.Domain.Warehouses;

namespace WebGalery.Domain.Databases.Factories
{
    public interface IDepotFactory
    {
        Depot BuildDefaultFor(Depository parent);
    }
}
