using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.Dtos
{
    public class ContentDto : HashedElementDto
    {
        public Stream Content { get; set; }
        public string FileName { get => Label; set => Label = value; }
    }
}
