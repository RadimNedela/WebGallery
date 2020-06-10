using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;

namespace Domain.Elements
{
    public class HashedElement
    {
        public long ID { get; set; }
        public string Hash { get; set; }
        public List<string> Locations { get; set; }
        public List<string> Tags { get; set; }

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