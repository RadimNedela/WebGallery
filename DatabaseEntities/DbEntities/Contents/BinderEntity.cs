using System.Collections.Generic;
using WebGalery.Core.DbEntities.Maintenance;

namespace WebGalery.Core.DbEntities.Contents
{
    public class BinderEntity : IDatabaseEntity
    {
        public string Hash { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }

        public IList<BinderEntityToContentEntity> Contents { get; set; }
        public IList<AttributedBinderEntityToContentEntity> AttributedContents { get; set; }

        public RackEntity Rack { get; set; }
        public string RackHash { get; set; }
    }
}