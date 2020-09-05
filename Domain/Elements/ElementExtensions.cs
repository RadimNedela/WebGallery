using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Elements
{
    public static class ElementExtensions
    {
        public static DisplayableInfoDto ToDisplayableInfoDto(this IEnumerable<HashedElement> hashedElements)
        {
            var hashedElementsList = hashedElements.ToList();
            List<BinderDto> binders = hashedElementsList.Where(he => he is BinderElement).Select(he => ((BinderElement)he).ToBinderDto()).ToList();
            List<ContentInfoDto> contents = hashedElementsList.Where(he => he is ContentElement).Select(he => ((ContentElement)he).ToContentInfoDto()).ToList();

            DisplayableInfoDto dto = new DisplayableInfoDto()
            {
                Binders = binders,
                ContentInfos = contents
            };

            return dto;
        }
    }
}
