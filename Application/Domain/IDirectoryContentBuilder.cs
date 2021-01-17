using System.Collections.Generic;

namespace WebGalery.FileImport.Domain
{
    public interface IDirectoryContentBuilder
    {
        IEnumerable<PhysicalFile> ParsePhysicalFiles(DirectoryContentThreadInfo info);
    }
}