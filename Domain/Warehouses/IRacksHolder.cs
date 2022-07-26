namespace WebGalery.Domain.Warehouses
{
    public interface IRacksHolder
    {
        IReadOnlySet<Rack> Racks { get; }
        void AddRack(Rack rack);
    }
}
