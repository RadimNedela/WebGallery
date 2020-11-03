using System.Collections.Generic;

namespace Domain.Dtos
{
    public class DirectoryInfoDto
    {
        public IEnumerable<string> SubDirectories { get; set; }
        public IEnumerable<string> Files { get; set; }
    }
}
