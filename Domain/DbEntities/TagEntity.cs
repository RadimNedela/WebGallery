using System.Collections;

namespace Domain.DbEntities
{
    public abstract class TagEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}