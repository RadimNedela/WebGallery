using System.Collections.Generic;

namespace WebGalery.Core.FileImport
{
    public class DirectoryContentThreadInfo
    {
        private IEnumerable<string> _fileNames;

        public DirectoryContentThreadInfoDto ThreadInfoDto { get; internal set; } 

        public string FullPath { get; internal set; }

        public IEnumerable<string> FileNames
        {
            get
            {
                foreach (var fileName in _fileNames)
                {
                    ThreadInfoDto.FilesDone++;
                    yield return fileName;
                }
            }
            internal set
            {
                _fileNames = value;
            }
        }

        public IEnumerable<string> DirNames { get; internal set; }
    }
}