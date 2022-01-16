namespace WebGalery.Domain.Databases
{
    public class Database : IHashedEntity
    {
        public string Name { get; set; } = "Default Database Name";

        public string Hash { get; protected internal set; } =
            "Default Database Hash String - too long to be used in DB, and also not really a hash string, " +
            "just some default to be used in tests and so on";

        public IList<Rack> Racks { get; protected internal set; } = new List<Rack>();

        public Rack DefaultRack => Racks.First();
    }
}
