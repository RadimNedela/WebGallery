namespace Domain.Services
{
    public interface IDatabaseInfoInitializer
    {
        void SetCurrentInfo(string rackHash);
    }
}
