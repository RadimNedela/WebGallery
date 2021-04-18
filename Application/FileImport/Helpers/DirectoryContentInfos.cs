using System.Collections.Generic;
using WebGalery.Core.FileImport;

namespace WebGalery.Application.FileImport.Helpers
{
    internal static class DirectoryContentInfos
    {
        internal static IDictionary<string, DirectoryContentThreadInfo> ContentInfos { get; } = new Dictionary<string, DirectoryContentThreadInfo>();
    }
}