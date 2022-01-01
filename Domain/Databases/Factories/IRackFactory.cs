namespace WebGalery.Domain.Databases.Factories
{
    public interface IRackFactory
    {
        Rack CreateFor(IHashedEntity parent);
    }
}
