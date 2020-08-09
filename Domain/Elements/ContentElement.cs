using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Elements
{
    public class ContentElement : HashedElement
    {
        public const string ImageType = "Image";
        public const string UnknownType = "Unknown";

        public ContentDto ToContentDto()
        {
            return new ContentDto()
            {
                Hash = Hash,
                Label = Label,
                Type = Type
            };
        }
    }
}
