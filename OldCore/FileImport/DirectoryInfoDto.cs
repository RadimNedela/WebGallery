using System.Collections.Generic;

namespace WebGalery.Core.FileImport
{
    public class DirectoryInfoDto
    {
        public string CurrentDirectory { get; set; }
        public IEnumerable<string> SubDirectories { get; set; }
        public IEnumerable<string> Files { get; set; }
    }
}
