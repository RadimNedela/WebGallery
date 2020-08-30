using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class DisplayableInfoDto
    {
        public List<BinderDto> Binders { get; set; }
        public List<ContentInfoDto> Contents { get; set; }
    }
}
