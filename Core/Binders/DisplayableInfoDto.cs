using System.Collections.Generic;

namespace WebGalery.Core.Binders
{
    public class DisplayableInfoDto
    {
        public BinderDto TheBinder { get; set; }
        public List<BinderDto> Binders { get; set; }
        public List<ContentInfoDto> ContentInfos { get; set; }
    }
}
