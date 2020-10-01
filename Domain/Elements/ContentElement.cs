using Domain.Dtos;
using System.Collections.Generic;
using System.IO;

namespace Domain.Elements
{

    public class ContentElement : HashedElement
    {
        public const string ImageType = "Image";
        public const string UnknownType = "Unknown";
        private IList<BinderElement> binders = new List<BinderElement>();
        private ISet<AttributedBinder> attributedBinders = new HashSet<AttributedBinder>();

        public string LastSeenFileFullPath { get; private set; }
        public IEnumerable<BinderElement> Binders => binders;
        public IEnumerable<AttributedBinder> AttributedBinders => attributedBinders;

        public ContentElement(string hash, BinderElement directoryBinder, string fullFilePath)
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
            var attBinder = new AttributedBinder(directoryBinder, fileName);
            if (!attributedBinders.Contains(attBinder))
                attributedBinders.Add(attBinder);
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
    }
}
