using System.Collections.Generic;

namespace Domain.Dtos
{
    public class DisplayableInfoDto
    {
        public BinderDto TheBinder { get; set; }
        public List<BinderDto> Binders { get; set; }
        public List<ContentInfoDto> ContentInfos { get; set; }
    }
}
