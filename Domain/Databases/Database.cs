namespace WebGalery.Domain.Databases
{
    public class Database : IHashedEntity
    {
        public string Name { get; set; } = null!;

        public string Hash { get; protected internal set; } = null!;

        public IList<IRack> Racks { get; protected internal set; } = new List<IRack>();
    }
}
