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
    }
}
