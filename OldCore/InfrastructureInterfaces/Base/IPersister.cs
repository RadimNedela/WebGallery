namespace WebGalery.Core.InfrastructureInterfaces.Base
{
    public interface IPersister<T> where T : IPersistable
    {
        void Add(T entity);

        void Remove(T entity);

        void Save();
    }
}