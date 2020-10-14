namespace Domain.DbEntities
{
    public abstract class HashedContentBaseEntity : HashedEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }
    }
}