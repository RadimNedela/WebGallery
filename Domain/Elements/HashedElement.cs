using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;
using Domain.DbEntities;
using Domain.InfrastructureInterfaces;

namespace Domain.Elements
{
    public abstract class HashedElement
    {
        public string Hash { get; internal set; }
        public string Type { get; internal set; }
        public string Label { get; internal set; }

        protected void InitializeDto(HashedElementDto dto)
        {
            dto.Hash = Hash;
            dto.Type = Type;
            dto.Label = Label;
        }
    }
}