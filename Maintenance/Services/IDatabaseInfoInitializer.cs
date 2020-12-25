namespace WebGalery.Maintenance.Services
{
    public interface IDatabaseInfoInitializer
    {
        void SetCurrentInfo(string rackHash);
    }
}
