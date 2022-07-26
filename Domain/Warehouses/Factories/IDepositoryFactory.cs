namespace WebGalery.Domain.Warehouses.Factories
{
    public interface IDepositoryFactory
    {
        Depository Build(string name);
    }
}
