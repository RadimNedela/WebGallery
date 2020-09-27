using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.Dtos
{
    public class ContentInfoDto : HashedElementDto
    {
        public string FilePath { get; set; }
    }
}
