namespace WebGalery.Domain.Databases.Factories
{
    public interface IRackFactory
    {
        Rack CreateFor(Database parent);
    }
}
