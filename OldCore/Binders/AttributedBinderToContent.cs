namespace WebGalery.Core.DbEntities.Contents
{
    public class AttributedBinderToContent : IPersistable
    {
        public string BinderHash { get; set; }
        public Binder Binder { get; set; }

        public string ContentHash { get; set; }
        public Content Content { get; set; }

        public string Attribute { get; set; }
    }
}