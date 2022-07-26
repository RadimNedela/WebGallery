using System.Linq;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.FileImport
{
    public class DirectoryContentThreadInfoFactory
    {
        private readonly IDirectoryMethods _directoryMethods;

        public DirectoryContentThreadInfoFactory(IDirectoryMethods directoryMethods)
        {
            _directoryMethods = directoryMethods;
        }

        public DirectoryContentThreadInfo Build(string fullPath)
        {
            var entity = new DirectoryContentThreadInfo()
            {
                FullPath = fullPath,
                FileNames = _directoryMethods.GetFileNames(fullPath),
                ThreadInfoDto = new DirectoryContentThreadInfoDto()
                {
                    FilesDone = 0
                }
            };
            entity.ThreadInfoDto.Files = entity.FileNames.Count();

            return entity;
        }
    }
}