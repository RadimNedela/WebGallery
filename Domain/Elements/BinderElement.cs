using Domain.Dtos;
using System.Collections.Generic;
using System.Reflection;
using Domain.DbEntities;

namespace Domain.Elements
{
    public class BinderElement : HashedElement
    {
        private readonly ISet<ContentElement> _contents = new HashSet<ContentElement>();
        private readonly ISet<BinderElement> _binders = new HashSet<BinderElement>();
        private readonly ISet<AttributedBinderElement> _attributes = new HashSet<AttributedBinderElement>();
        private BinderEntity _binderEntity;

        public BinderTypeEnum BinderType { get; private set; }

        public IEnumerable<ContentElement> Contents => _contents;
        public IEnumerable<BinderElement> Binders => _binders;
        public IEnumerable<AttributedBinderElement> Attributes => _attributes;

        public BinderElement(string hash, BinderTypeEnum type, string label) 
        {
            base.Initialize(hash, type.ToString(), label);
            BinderType = type;
        }

        public void AddBinder(BinderElement binderElement)
        {
            if (_binders.Contains(binderElement))
                return;
            _binders.Add(binderElement);
        }

        public void AddContent(AttributedBinderElement attribute, ContentElement contentElement)
        {
            if (!_attributes.Contains(attribute))
                _attributes.Add(attribute);
            if (_contents.Contains(contentElement))
                return;
            _contents.Add(contentElement);
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

        public BinderEntity ToEntity()
        {

        }

        public BinderEntityToContentEntity ToEntity()
        {
            throw new System.NotImplementedException();
        }
    }
}
