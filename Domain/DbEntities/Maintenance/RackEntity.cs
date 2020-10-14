namespace Domain.DbEntities.Maintenance
{
    public class RackEntity
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public DatabaseInfoEntity Database { get; set; }
    }
}
