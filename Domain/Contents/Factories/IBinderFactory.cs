namespace WebGalery.Domain.Contents.Factories
{
    public interface IBinderFactory
    {
        Binder LoadDirectory(string directoryName);
    }
}
