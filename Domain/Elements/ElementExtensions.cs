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
            List<BinderDto> binders = hashedElements.Where(he => he is BinderElement).Select(he => ((BinderElement)he).ToBinderDto()).ToList();
            List<ContentDto> contents = hashedElements.Where(he => he is ContentElement).Select(he => ((ContentElement)he).ToContentDto()).ToList();

            DisplayableInfoDto dto = new DisplayableInfoDto()
            {
                Binders = binders,
                Contents = contents
            };

            return dto;
        }
    }
}
