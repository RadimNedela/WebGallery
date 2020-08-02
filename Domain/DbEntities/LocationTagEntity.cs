using System.Collections.Generic;

namespace Domain.DbEntities
{
    public class LocationTagEntity : TagEntity
    {
        public int Value { get; set; }
        public IList<LocationEntityToTagEntity> Locations { get; set; }
    }
}