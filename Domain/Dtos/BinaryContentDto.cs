using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Domain.Dtos
{
    public class BinaryContentDto : ContentInfoDto
    {
        public Stream Content { get; set; }
    }
}
