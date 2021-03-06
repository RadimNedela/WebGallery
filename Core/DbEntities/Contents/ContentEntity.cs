﻿using System.Collections.Generic;

namespace WebGalery.Core.DbEntities.Contents
{
    public class ContentEntity : IDatabaseEntity
    {
        public string Hash { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }

        public IList<BinderEntityToContentEntity> Binders { get; set; }
        public IList<AttributedBinderEntityToContentEntity> AttributedBinders { get; set; }
    }
}