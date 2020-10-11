using Domain.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Domain.DbEntities;

namespace Domain.Elements
{
    public class ContentElement : HashedElement
    {
        public const string ImageType = "Image";
        public const string UnknownType = "Unknown";
        private readonly IList<BinderElement> _binders = new List<BinderElement>();
        private readonly ISet<AttributedBinderElement> _attributedBinders = new HashSet<AttributedBinderElement>();
        private ContentEntity _contentEntity;

        public string LastSeenFileFullPath { get; private set; }
        public IEnumerable<BinderElement> Binders => _binders;
        public IEnumerable<AttributedBinderElement> AttributedBinders => _attributedBinders;

        internal ContentElement(string hash, BinderElement directoryBinder, string fullFilePath)
        {
            string type = GetFileType(fullFilePath);
            string fileName = Path.GetFileName(fullFilePath);

            base.Initialize(hash, type, fileName);
            AddLastSeenFilePosition(fullFilePath, directoryBinder, fileName);
        }

        private string GetFileType(string path)
        {
            string extension = System.IO.Path.GetExtension(path).ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return ContentElement.ImageType;
                default:
                    return ContentElement.UnknownType;
            }
        }

        public void AddLastSeenFilePosition(string fullFilePath, BinderElement directoryBinder)
        {
            AddLastSeenFilePosition(fullFilePath, directoryBinder, Path.GetFileName(fullFilePath));
        }

        private void AddLastSeenFilePosition(string fullFilePath, BinderElement directoryBinder, string fileName)
        {
            var attBinder = new AttributedBinderElement(directoryBinder, this, fileName);
            if (!_attributedBinders.Contains(attBinder))
                _attributedBinders.Add(attBinder);
            LastSeenFileFullPath = fullFilePath;
        }

        protected void InitializeDto(ContentInfoDto dto)
        {
            base.InitializeDto(dto);
            dto.FilePath = LastSeenFileFullPath;
        }

        public ContentInfoDto ToContentInfoDto()
        {
            var dto = new ContentInfoDto();
            InitializeDto(dto);
            return dto;
        }

        public ContentEntity ToEntity()
        {
            if (_contentEntity == null)
                _contentEntity = new ContentEntity
                {
                    Hash = Hash,
                    Type = Type
                };
            _contentEntity.Label = Label;
            if (_contentEntity.Binders != null)
            {
                // need to merge
            }
            else
            {
                _contentEntity.Binders = new List<BinderEntityToContentEntity>();
                foreach (var binderElement in Binders)
                {
                    _contentEntity.Binders.Add(binderElement.ToEntity());
                }
            }

            if (_contentEntity.AttributedBinders != null)
            {
                // need to merge
            }
            else
            {
                _contentEntity.AttributedBinders = new List<AttributedBinderEntityToContentEntity>();
                foreach (var attributedBinderElement in AttributedBinders)
                {
                    _contentEntity.AttributedBinders.Add(attributedBinderElement.ToEntity());
                }
            }
            return _contentEntity;
        }
    }
}
