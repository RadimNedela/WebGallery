namespace Domain.Elements
{
    public class AttributedBinderElement
    {
        public BinderElement TheBinder { get; private set; }
        public string Attribute { get; private set; }

        public AttributedBinderElement(BinderElement binder, ContentElement content, string attribute)
        {
            TheBinder = binder;
            Attribute = attribute;
            TheBinder.AddContent(this, content);
        }

        public override bool Equals(object other)
        {
            if (other is AttributedBinderElement otherBinder)
            {
                return 
                    TheBinder.Hash == otherBinder.TheBinder.Hash
                    && Attribute == otherBinder.Attribute;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return $"{TheBinder.Hash}-{Attribute}".GetHashCode();
        }
    }
}
