using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;
using Domain.Entities;
using Domain.InfrastructureInterfaces;

namespace Domain.Elements
{
    public class HashedElement
    {
        private readonly IHashedEntitiesRepository _repository;

        internal HashedElement(IHashedEntitiesRepository repository)
        {
            _repository = repository;
        }

        internal HashedElement InitializeUsing(string hash)
        {
            Hash = hash;
            return this;
        }

        internal HashedElement IsDirectory()
        {
            Type = "DIR";
            return this;
        }

        internal HashedElement IsFile()
        {
            Type = "FILE";
            return this;
        }

        internal HashedElement InitializeFrom(Hashed hashed)
        {
            Hash = hashed.Hash;
            _hashed = hashed;
            //Locations
            return this;
        }

        private Hashed _hashed;

        private List<string> locations;
        private List<string> tags;

        public string Hash { get; private set; }
        public string Type { get; private set; }
        public List<string> Locations
        {
            get
            {
                if (locations == null) locations = new List<string>();
                return locations;
            }
            private set => locations = value;
        }
        public List<string> Tags
        {
            get
            {
                if (tags == null) tags = new List<string>();
                return tags;
            }
            private set => tags = value;
        }

        public bool IsDisplayableAsImage
        {
            get
            {
                string path = Locations?.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(path))
                {
                    string upper = path.ToUpper();
                    return upper.EndsWith("JPG") || upper.EndsWith("JPEG");
                }
                return false;
            }
        }
        public void AddPath(string path)
        {
            if (!Locations.Contains(path))
                Locations.Add(path);
        }

        public FileInfoDto GetFileInfoDto()
        {
            var retVal = new FileInfoDto()
            {
                Checksum = Hash,
                IsDisplayableAsImage = IsDisplayableAsImage,
                FileName = Locations?.FirstOrDefault()
            };
            return retVal;
        }
    }
}