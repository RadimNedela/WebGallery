using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class DirectoryContentThreadInfo : DirectoryContentThreadInfoDto
    {
        public string FullPath { get; set; }
        public IEnumerable<string> FileNames
        {
            get => fileNames;
            set
            {
                fileNames = value;
                Files = fileNames.Count();
                FilesDone = 0;
            }
        }
        public IEnumerable<string> DirNames { get; set; }

        private IEnumerable<string> fileNames;
    }
}