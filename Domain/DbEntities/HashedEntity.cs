using System.Collections.Generic;

namespace Domain.DbEntities
{
    public class HashedEntity
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Type { get; set; }
        public IList<LocationEntityToHashedEntity> Locations { get; set; }
        public IList<HashedEntityToTagEntity> Tags { get; set; }
    }
}