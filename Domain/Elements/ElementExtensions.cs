using Domain.DbEntities;
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

        public static ContentEntity ToEntity(this ContentElement element)
        {
            var entity = new ContentEntity
            {
                Hash = element.Hash,
                Label = element.Label,
                Type = element.Type
            };

            return entity;
        }

        public static BinderEntity ToEntity(this BinderElement element)
        {
            var entity = new BinderEntity
            {
                Hash = element.Hash,
                Label = element.Label,
                Type = element.Type
            };
            return entity;
        }
    }
}
