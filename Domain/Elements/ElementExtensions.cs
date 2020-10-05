using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Elements
{
    public static class ElementExtensions
    {
        public static DisplayableInfoDto ToDisplayableInfoDto(this BinderElement binderElement)
        {
            List<BinderDto> binders = binderElement.Binders.Select(he => ((BinderElement)he).ToBinderDto()).ToList();
            List<ContentInfoDto> contents = binderElement.Contents.Select(he => ((ContentElement)he).ToContentInfoDto()).ToList();

            DisplayableInfoDto dto = new DisplayableInfoDto()
            {
                Binders = binders,
                ContentInfos = contents
            };

            return dto;
        }
    }
}
