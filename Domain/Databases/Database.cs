namespace WebGalery.Domain.Databases
{
    public class Database : IHashedEntity
    {
        private IRack? currentRack;

        public string Name { get; set; } = "Default Database Name";

        public string Hash { get; protected internal set; } =
            "Default Database Hash String - too long to be used in DB, and also not really a hash string, " +
            "just some default to be used in tests and so on";

        public IList<IRack> Racks { get; protected internal set; } = new List<IRack>();

        public IRack CurrentRack
        {
            get => currentRack ??= Racks.First();
            set => currentRack = value;
        }
    }
}
