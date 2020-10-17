using Domain.DbEntities.Maintenance;
using System.Collections.Generic;

namespace Domain.DbEntities
{
    public class BinderEntity: HashedContentBaseEntity
    {
        public IList<BinderEntityToContentEntity> Contents { get; set; }
        public IList<AttributedBinderEntityToContentEntity> AttributedContents { get; set; }
        public RackEntity Rack { get; set; }
    }
}