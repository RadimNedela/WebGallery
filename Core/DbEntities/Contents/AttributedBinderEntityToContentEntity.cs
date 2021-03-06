﻿namespace WebGalery.Core.DbEntities.Contents
{
    public class AttributedBinderEntityToContentEntity : IDatabaseEntity
    {
        public string BinderHash { get; set; }
        public BinderEntity Binder { get; set; }

        public string ContentHash { get; set; }
        public ContentEntity Content { get; set; }

        public string Attribute { get; set; }
    }
}