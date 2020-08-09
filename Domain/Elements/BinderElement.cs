using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Elements
{
    public class BinderElement : HashedElement
    {
        public const string DirectoryType = "Directory";

        public IList<BinderElement> Binders { get; set; }
        public IList<ContentElement> Contents { get; set; }

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
