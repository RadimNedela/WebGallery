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

        public Stream ContentStream { get; internal set; }

        public IList<BinderElement> Binders { get; set; }

        protected void InitializeDto(ContentInfoDto dto)
        {
            base.InitializeDto(dto);
            dto.FileName = Label;
        }

        protected void InitializeDto(BinaryContentDto dto)
        {
            InitializeDto((ContentInfoDto)dto);
            dto.Content = ContentStream;
        }

        public ContentInfoDto ToContentInfoDto()
        {
            var dto = new ContentInfoDto();
            InitializeDto(dto);
            return dto;
        }

        public BinaryContentDto ToBinaryContentDto()
        {
            var dto = new BinaryContentDto();
            InitializeDto(dto);
            return dto;
        }
    }
}
