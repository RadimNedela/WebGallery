namespace WebGalery.Domain.Contents.Factories
{
    public interface IDirectoryBinderFactory
    {
        DirectoryBinder LoadDirectory(string directoryName);
    }
}
