using System.Collections.Generic;

namespace Domain.Domain
{
    public class FileInfo
    {
        public string Hash { get; set; }
        public IEnumerable<string> Locations { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}