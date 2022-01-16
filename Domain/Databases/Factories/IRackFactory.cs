namespace WebGalery.Domain.Databases.Factories
{
    public interface IRackFactory
    {
        Rack CreateDefaultFor(Database parent);
    }
}
