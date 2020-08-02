using System.Collections;
using System.Collections.Generic;

namespace Domain.DbEntities
{
    public class LocationEntity
    {
        public int Id { get; set; }
        public string Directory { get; set; }
        public string FileName { get; set; }
        public IList<LocationEntityToTagEntity> Tags { get; set; }
    }
}