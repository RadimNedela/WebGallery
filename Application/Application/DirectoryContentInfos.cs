using System.Collections.Generic;
using WebGalery.FileImport.Domain;

namespace WebGalery.FileImport.Application
{
    public static class DirectoryContentInfos
    {
        public static IDictionary<string, DirectoryContentThreadInfo> ContentInfos { get; } = new Dictionary<string, DirectoryContentThreadInfo>();
    }
}