using System;
using System.Linq;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.Maintenance
{
    class RackService
    {
        private readonly IDirectoryMethods _directoryMethods;

        public RackService(IDirectoryMethods directoryMethods)
        {
            _directoryMethods = directoryMethods;
        }

        public string GetActiveDirectory(Rack rack)
        {
            return rack.MountPoints.First(mp => _directoryMethods.Exists(mp.Path)).Path;
        }

        public string GetSubpath(Rack rack, string fullPath)
        {
            var normalizedFullPath = _directoryMethods.NormalizePath(fullPath);
            foreach (var mountPoint in rack.MountPoints)
            {
                var normalizedMountPoint = _directoryMethods.NormalizePath(mountPoint.Path);
                if (normalizedFullPath.StartsWith(normalizedMountPoint, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (normalizedFullPath.Length == normalizedMountPoint.Length)
                        return ".";
                    return normalizedFullPath.Substring(normalizedMountPoint.Length + 1);
                }
            }
            throw new Exception($"Sorry, the given path {fullPath} does not start with any known mount point");
        }
    }
}
