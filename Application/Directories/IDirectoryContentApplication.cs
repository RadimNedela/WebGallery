using System.Collections.Generic;
using Domain.Dtos;

namespace Application.Directories
{
    public interface IDirectoryContentApplication
    {
        IEnumerable<DirectoryElementDto> GetDirectoryContent(string path);
    }
}