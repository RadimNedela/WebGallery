using System;
using System.IO;
using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.DBMaintenanceInterfaces;

namespace WebGalery.Maintenance.Domain
{
    public class Rack : IRack
    {
        private readonly RackEntity _entity;

        public Rack(RackEntity entity)
        {
            _entity = entity;
        }

        public string Hash => _entity.Hash;

        public string Name => _entity.Name;

        public string ActiveDirectory => _entity.MountPoints.First(mp => Directory.Exists(mp.Path)).Path;

        public string GetSubpath(string fullPath)
        {
            var normalizedFullPath = NormalizePath(fullPath);
            foreach (var mountPoint in _entity.MountPoints)
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
