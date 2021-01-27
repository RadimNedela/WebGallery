using System.Collections.Generic;
using WebGalery.FileImport.Domain;

namespace WebGalery.FileImport.Application.Helpers
{
    internal static class DirectoryContentInfos
    {
        internal static IDictionary<string, DirectoryContentThreadInfo> ContentInfos { get; } = new Dictionary<string, DirectoryContentThreadInfo>();
    }
}