using Domain.Dtos;
using Domain.Elements;

namespace Application.Directories
{
    public static class DomainExtensions
    {
        public static BinderDto ToBinderDto(this BinderElement element)
        {
            return new BinderDto
            {
                Hash = element.Hash,
                Label = element.Label,
                Type = element.Type
            };
        }

        public static void InitializeDto(this HashedElement element, HashedElementDto dto)
        {
            dto.Hash = element.Hash;
            dto.Type = element.Type;
            dto.Label = element.Label;
        }



        public static ContentInfoDto ToContentInfoDto(this ContentElement element)
        {
            var dto = new ContentInfoDto();

            element.InitializeDto(dto);
            dto.FilePath = element.LastSeenFileFullPath;


            return dto;
        }


    }
}