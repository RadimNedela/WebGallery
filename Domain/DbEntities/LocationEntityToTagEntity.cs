namespace Domain.DbEntities
{
    public class LocationEntityToTagEntity
    {
        public int TagId { get; set; }
        public LocationTagEntity Tag { get; set; }

        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
    }
}