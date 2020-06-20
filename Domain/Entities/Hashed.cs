using System.Collections.Generic;

namespace Domain.Entities
{
    public class Hashed
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Type { get; set; }
        public List<Location> Locations { get; set; }
        public List<Tag> Tags { get; set; }
    }
}