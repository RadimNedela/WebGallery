using System.Collections.Generic;
using System.Linq;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.FileImport
{
    public class DirectoryContentThreadInfo
    {
        private readonly IDirectoryMethods _directoryMethods;
        private IEnumerable<string> _fileNames;
        private string _fullPath;

        public DirectoryContentThreadInfo(IDirectoryMethods directoryMethods)
        {
            _directoryMethods = directoryMethods;
        }

        public DirectoryContentThreadInfoDto ThreadInfoDto { get; set; } = new DirectoryContentThreadInfoDto();

        public string FullPath
        {
            get { return _fullPath; }
            set
            {
                _fullPath = value;
                _fileNames = _directoryMethods.GetFileNames(_fullPath);
                ThreadInfoDto.Files = _fileNames.Count();
                ThreadInfoDto.FilesDone = 0;
            }
        }

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
        }

        public IEnumerable<string> DirNames { get; set; }

    }
}