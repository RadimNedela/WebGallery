using System.Collections.Generic;
using Domain.Dtos;

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

        public override bool Equals(object obj)
        {
            if (obj is HashedElement he)
                return Hash.Equals(he.Hash);
            return false;
        }

        public override int GetHashCode()
        {
            return -1545866855 + EqualityComparer<string>.Default.GetHashCode(Hash);
        }
    }
}