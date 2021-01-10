using System.Collections.Generic;

namespace WebGalery.FileImport.Domain
{
    public interface IDirectoryContentBuilder
    {
        IEnumerable<PhysicalFile> PhysicalFiles();
    }
}