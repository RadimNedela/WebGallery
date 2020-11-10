﻿using System.Collections.Generic;

namespace Domain.Dtos
{
    public class DirectoryInfoDto
    {
        public string CurrentDirectory { get; set; }
        public IEnumerable<string> SubDirectories { get; set; }
        public IEnumerable<string> Files { get; set; }
    }
}
