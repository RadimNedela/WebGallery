namespace Domain.Elements
{
    public class AttributedBinder
    {
        public BinderElement TheBinder { get; private set; }
        public string Attribute { get; private set; }

        public AttributedBinder(BinderElement binder, ContentElement content, string attribute)
        {
            TheBinder = binder;
            Attribute = attribute;
            TheBinder.AddContent(content);
        }

        public override bool Equals(object other)
        {
            if (other is AttributedBinder otherBinder)
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
