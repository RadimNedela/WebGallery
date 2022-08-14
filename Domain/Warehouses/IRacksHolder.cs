namespace WebGalery.Domain.Warehouses
{
    public interface IRacksHolder
    {
        IEnumerable<RackBase> Racks { get; }
        void AddRack(RackBase rack);
    }
}
