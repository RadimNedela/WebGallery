namespace Domain.DbEntities
{
    public class LocationEntityToHashedEntity
    {
        public int HashedId { get; set; }
        public HashedEntity Hashed { get; set; }

        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
    }
}