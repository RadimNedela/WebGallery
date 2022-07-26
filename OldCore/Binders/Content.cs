using System.Collections.Generic;

namespace WebGalery.Core.DbEntities.Contents
{
    public class Content : IPersistable
    {
        public string Hash { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }

        public IList<BinderToContent> Binders { get; set; }
        public IList<AttributedBinderToContent> AttributedBinders { get; set; }
    }
}