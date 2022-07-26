namespace WebGalery.Core.InfrastructureInterfaces.Base
{
    public interface IRepository<T> where T : IPersistable
    {
        T Get(string hash);
    }
}