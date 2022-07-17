using WebGalery.Domain.Warehouses;

namespace WebGalery.Domain.Databases.Factories
{
    public interface IDepositoryFactory
    {
        Depository Build(string name);
    }
}
