namespace Domain.DbEntities
{
    public class HashedEntityToTagEntity
    {
        public int TagId { get; set; }
        public HashedTagEntity Tag { get; set; }

        public int HashedId { get; set; }
        public HashedEntity Hashed { get; set; }
    }
}