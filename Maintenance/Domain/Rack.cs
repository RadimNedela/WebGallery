using System;
using System.IO;
using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.DBMaintenanceInterfaces;

namespace WebGalery.Maintenance.Domain
{
    public class Rack : IRack
    {
        private readonly RackEntity entity;

        public Rack(RackEntity entity)
        {
            this.entity = entity;
        }

        public string Name => entity.Name;

        public string ActiveDirectory => entity.MountPoints.First(mp => Directory.Exists(mp.Path)).Path;

        public string GetSubpath(string fullPath)
        {
            var normalizedFullPath = NormalizePath(fullPath);
            foreach (var mountPoint in entity.MountPoints)
            {
                var normalizedMountPoint = NormalizePath(mountPoint.Path);
                if (normalizedFullPath.StartsWith(normalizedMountPoint, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (normalizedFullPath.Length == normalizedMountPoint.Length)
                        return ".";
                    return normalizedFullPath.Substring(normalizedMountPoint.Length + 1);
                }
            }
            throw new Exception($"Sorry, the given path {fullPath} does not start with any known mount point");
        }

        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            //.ToUpperInvariant();
        }
    }
}
