using System.Collections.Generic;
using System.Linq;
using WebGalery.FileImport.Application.Dtos;

namespace WebGalery.FileImport.Domain
{
    public class DirectoryContentThreadInfo : DirectoryContentThreadInfoDto
    {
        public string FullPath { get; set; }
        public IEnumerable<string> FileNames
        {
            get => _fileNames;
            set
            {
                _fileNames = value;
                Files = _fileNames.Count();
                FilesDone = 0;
            }
        }
        public IEnumerable<string> DirNames { get; set; }

        private IEnumerable<string> _fileNames;
    }
}