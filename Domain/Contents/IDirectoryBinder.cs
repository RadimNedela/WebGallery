namespace WebGalery.Domain.Contents
{
    public interface IDirectoryBinder
    {
        IHashedEntity Parent { get; }
    }
}