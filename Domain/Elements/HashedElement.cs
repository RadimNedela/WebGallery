using System.Collections.Generic;
using Domain.DbEntities;
using Domain.Dtos;

namespace Domain.Elements
{
    public abstract class HashedElement
    {
        public int? Id { get; private set; }
        public string Hash { get; private set; }
        public string Type { get; private set; }
        public string Label { get; private set; }

        protected void Initialize(int id, string hash, string type, string label)
        {
            Id = id;
            Hash = hash;
            Type = type;
            Label = label;
        }

        protected void Initialize(string hash, string type, string label)
        {
            Hash = hash;
            Type = type;
            Label = label;
        }

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