using System.Collections.Generic;
using Domain.Services;

namespace Application.Directories
{
    public static class DirectoryContentInfos
    {
        public static IDictionary<string, DirectoryContentThreadInfo> ContentInfos { get; } = new Dictionary<string, DirectoryContentThreadInfo>();
    }
}