using System.Collections.Generic;

namespace Domain.DbEntities
{
    public class BinderEntity: HashedEntity
    {
        public IList<BinderEntityToContentEntity> Contents { get; set; }
    }
}