using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Domain.Elements
{
    public class ContentElement : HashedElement
    {
        public const string ImageType = "Image";
        public const string UnknownType = "Unknown";

        public string FileFullPath { get; internal set; }

        public IList<BinderElement> Binders { get; set; }

        protected void InitializeDto(ContentInfoDto dto)
        {
            base.InitializeDto(dto);
            dto.FilePath = FileFullPath;
        }

        public ContentInfoDto ToContentInfoDto()
        {
            var dto = new ContentInfoDto();
            InitializeDto(dto);
            return dto;
        }
    }
}
