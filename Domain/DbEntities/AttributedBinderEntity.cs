using System.Collections.Generic;

namespace Domain.DbEntities
{
    public class AttributedBinderEntity: HashedEntity
    {
        public IList<AttributedBinderEntityToContentEntity> AttributedContents { get; set; }
    }
}