using Domain.Dtos;
using System.Collections.Generic;
using System.Reflection;
using Domain.DbEntities;
using System;

namespace Domain.Elements
{
    public class BinderElement : HashedElement
    {
        private readonly ISet<ContentElement> _contents = new HashSet<ContentElement>();
        private readonly ISet<BinderElement> _binders = new HashSet<BinderElement>();
        private readonly IDictionary<string, ContentElement> _attributes = new Dictionary<string, ContentElement>();
        private BinderEntity _binderEntity;

        public BinderTypeEnum BinderType { get; private set; }

        public IEnumerable<ContentElement> Contents => _contents;
        public IEnumerable<BinderElement> Binders => _binders;
        public IEnumerable<KeyValuePair<string, ContentElement>> Attributes => _attributes;

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

        public void AddContent(string attribute, ContentElement contentElement)
        {
            if (_attributes.ContainsKey(attribute))
                throw new NotSupportedException("Currently it is not supported for Binder to contain 2 same attributes");

            _attributes.Add(attribute, contentElement);
            if (!_contents.Contains(contentElement))
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

        internal AttributedBinderEntityToContentEntity ToEntity(AttributedBinderEntityToContentEntity bToC)
        {
            if (_binderEntity == null)
            {
                _binderEntity = new BinderEntity
                {
                    Hash = Hash,
                    Label = Label,
                    Type = Type,
                    //AttributedContents = new List<AttributedBinderEntityToContentEntity>(),
                    //Contents = new List<BinderEntityToContentEntity>()
                };
            }
            if (bToC.Binder != null && bToC.Binder != _binderEntity)
                throw new Exception("Something totaly wrong - bToC contains other binder as this one...");
            if (bToC.Binder == null)
                bToC.Binder = _binderEntity;

            return bToC;
        }
    }
}
