using Domain.Dtos;

namespace Domain.Elements
{
    public class BinderElement : HashedElement
    {
        public BinderTypeEnum BinderType { get; private set; }

        public BinderElement(string hash, BinderTypeEnum type, string label) 
        {
            base.Initialize(hash, type.ToString(), label);
            BinderType = type;
        }

        public BinderDto ToBinderDto()
        {
            return new BinderDto()
            {
                Hash = Hash,
                Label = Label,
                Type = Type
            };
        }
    }
}
