namespace Domain.DbEntities
{
    public abstract class HashedEntity
    {
        public int Id { get; set; }

        public string Hash { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }
    }
}