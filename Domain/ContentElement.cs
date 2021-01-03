﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.DbEntities;
using WebGalery.DatabaseEntities.Contents;

namespace Domain.Elements
{
    public class ContentElement : HashedElement
    {
        private readonly IList<BinderElement> _binders = new List<BinderElement>();
        private readonly IList<KeyValuePair<string, BinderElement>> _attributedBinders = new List<KeyValuePair<string, BinderElement>>();
        private ContentEntity _contentEntity;

        public string LastSeenFileFullPath { get; private set; }
        public IEnumerable<BinderElement> Binders => _binders;
        public IEnumerable<KeyValuePair<string, BinderElement>> AttributedBinders => _attributedBinders;

        internal ContentElement(string hash, BinderElement directoryBinder, string fullFilePath)
        {
            ContentTypeEnum type = GetFileType(fullFilePath);
            string fileName = Path.GetFileName(fullFilePath);

            Initialize(hash, type.ToString(), fileName);
            AddLastSeenFilePosition(fullFilePath, directoryBinder, fileName);
        }

        private ContentTypeEnum GetFileType(string path)
        {
            string extension = Path.GetExtension(path).ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return ContentTypeEnum.ImageType;
                default:
                    return ContentTypeEnum.UnknownType;
            }
        }

        public void AddLastSeenFilePosition(string fullFilePath, BinderElement directoryBinder)
        {
            AddLastSeenFilePosition(fullFilePath, directoryBinder, Path.GetFileName(fullFilePath));
        }

        private void AddLastSeenFilePosition(string fullFilePath, BinderElement directoryBinder, string fileName)
        {
            var newKeyValue = new KeyValuePair<string, BinderElement>(fileName, directoryBinder);
            if (!_attributedBinders.Any(a => a.Key == newKeyValue.Key && a.Value == newKeyValue.Value))
            {
                _attributedBinders.Add(newKeyValue);
                directoryBinder.AddContent(fileName, this);
            }
            LastSeenFileFullPath = fullFilePath;
        }

        public ContentEntity ToEntity()
        {
            _contentEntity ??= new ContentEntity
            {
                Hash = Hash,
                Type = Type
            };
            _contentEntity.Label = Label;
            if (_contentEntity.AttributedBinders != null)
            {
                // need to merge
            }
            else
            {
                _contentEntity.AttributedBinders = new List<AttributedBinderEntityToContentEntity>();
                foreach (var keyValue in AttributedBinders)
                {
                    var bToC = new AttributedBinderEntityToContentEntity
                    {
                        Content = _contentEntity,
                        Attribute = keyValue.Key
                    };

                    _contentEntity.AttributedBinders.Add(keyValue.Value.ToEntity(bToC));
                }
            }

            return _contentEntity;
        }
    }
}