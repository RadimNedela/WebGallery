namespace WebGalery.Domain.Warehouses.Factories
{
    public interface IDepotFactory
    {
        Depot BuildDefaultFor(Depository parent);
    }
}
