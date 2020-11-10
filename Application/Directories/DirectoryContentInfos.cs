using Domain.Services;
using System.Collections.Generic;

namespace Application.Directories
{
    public static class DirectoryContentInfos
    {
        public static IDictionary<string, DirectoryContentThreadInfo> ContentInfos { get; } = new Dictionary<string, DirectoryContentThreadInfo>();
    }
}