namespace Domain.DbEntities
{
    public class AttributedBinderEntityToContentEntity
    {
        public int Id { get; set; }

        public int AttributedBinderId { get; set; }
        public AttributedBinderEntity AttributedBinder { get; set; }

        public int ContentId { get; set; }
        public ContentEntity Content { get; set; }

        public string Attribute { get; set; }
    }
}