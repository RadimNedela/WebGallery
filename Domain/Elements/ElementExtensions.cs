using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;

namespace Domain.Elements
{
    public static class ElementExtensions
    {
        public static DisplayableInfoDto ToDisplayableInfoDto(this BinderElement binderElement)
        {
            List<BinderDto> binders = binderElement.Binders.Select(he => he.ToBinderDto()).ToList();
            List<ContentInfoDto> contents = binderElement.Contents.Select(he => he.ToContentInfoDto()).ToList();

            DisplayableInfoDto dto = new DisplayableInfoDto
            {
                TheBinder = binderElement.ToBinderDto(),
                Binders = binders,
                ContentInfos = contents
            };

            return dto;
        }

    }
}
