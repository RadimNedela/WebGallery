using Domain.Dtos;
using System.Collections.Generic;

namespace Domain.Elements
{
    public class BinderElement : HashedElement
    {
        private ISet<ContentElement> contents = new HashSet<ContentElement>();
        private ISet<BinderElement> binders = new HashSet<BinderElement>();

        public BinderTypeEnum BinderType { get; private set; }

        public IEnumerable<ContentElement> Contents => contents;
        public IEnumerable<BinderElement> Binders => binders;

        public BinderElement(string hash, BinderTypeEnum type, string label) 
        {
            base.Initialize(hash, type.ToString(), label);
            BinderType = type;
        }

        public void AddBinder(BinderElement binderElement)
        {
            if (binders.Contains(binderElement))
                return;
            binders.Add(binderElement);
        }

        public void AddContent(ContentElement contentElement)
        {
            if (contents.Contains(contentElement))
                return;
            contents.Add(contentElement);
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
