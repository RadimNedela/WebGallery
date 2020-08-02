using System.Collections.Generic;

namespace Domain.DbEntities
{
    public class HashedTagEntity : TagEntity
    {
        public IList<HashedEntityToTagEntity> Hasheds { get; set; }
    }
}