namespace Domain.DbEntities.Maintenance
{
    public class RackEntity : HashedEntity
    {
        public string Name { get; set; }
        public DatabaseInfoEntity Database { get; set; }
    }
}
