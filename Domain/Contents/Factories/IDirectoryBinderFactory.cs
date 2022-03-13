namespace WebGalery.Domain.Contents.Factories
{
    public interface IDirectoryBinderFactory
    {
        Binder LoadDirectory(string directoryName);
    }
}
