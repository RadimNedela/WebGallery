using System.Collections.Generic;

namespace Domain.DbEntities
{
    public class ContentEntity : HashedContentBaseEntity
    {
        public IList<BinderEntityToContentEntity> Binders { get; set; }
        public IList<AttributedBinderEntityToContentEntity> AttributedBinders { get; set; }
    }
}