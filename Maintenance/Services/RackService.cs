using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.DbEntities.Maintenance;
using Domain.Dtos.Maintenance;
using Domain.Services.InfrastructureInterfaces;

namespace Domain.Elements.Maintenance
{
    public class RackService
    {
        //private readonly IHasher _hasher;
        //public RackEntity Entity { get; }

        //public string Hash { get; }
        //public string Name { get; private set; }
        //public List<string> MountPoints { get; }

        //public RackService(IHasher hasher, RackEntity entity)
        //{
        //    _hasher = hasher;
        //    Entity = entity;
        //    Hash = entity.Hash;
        //    Name = entity.Name;
        //    MountPoints = entity.MountPoints.Select(mpe => mpe.Path).ToList();
        //}

        //public RackService(DatabaseInfoEntity diEntity, IHasher hasher, string name, IEnumerable<string> initialMountPoints)
        //{
        //    _hasher = hasher;
        //    Name = name;
        //    MountPoints = new List<string>();

        //    // buďto budu počítat hash při změně jména (což by asi mělo katastrofální dopad), nebo to bude muset být náhodné...
        //    string rackHash = hasher.ComputeRandomStringHash($"{diEntity.Hash} {name}");

        //    Entity = new RackEntity
        //    {
        //        Database = diEntity,
        //        Hash = rackHash,
        //        Name = name,
        //        MountPoints = new List<MountPointEntity>()
        //    };

        //    foreach (var mountPoint in initialMountPoints)
        //    {
        //        AddMountPoint(mountPoint);
        //    }
        //}

        //public void AddMountPoint(string newMountPoint)
        //{
        //    if (string.IsNullOrWhiteSpace(newMountPoint))
        //        throw new Exception("Mount point cannot be null nor empty string...");
        //    if (MountPoints.Contains(newMountPoint))
        //        throw new Exception("Cannot create same mount point - this one already exists");

        //    MountPoints.Add(newMountPoint);

        //    var mountPointEntity = new MountPointEntity
        //    {
        //        Path = newMountPoint,
        //        Rack = Entity
        //    };
        //    Entity.MountPoints.Add(mountPointEntity);
        //}

        //internal void Merge(RackDto rackDto)
        //{
        //    if (Hash != rackDto.Hash)
        //        throw new Exception($"Tož to né - haš nesouhlasí {rackDto.Hash} != {this}");
        //    if (Name != rackDto.Name)
        //    {
        //        Name = rackDto.Name;
        //        Entity.Name = Name;
        //    }

        //    // nejprve smažeme...
        //    for (int i = MountPoints.Count - 1; i >= 0; i--)
        //    {
        //        if (rackDto.MountPoints.Contains(MountPoints[i]))
        //            continue;
        //        // není v dto seznamu, smaž ho
        //        MountPoints.RemoveAt(i);
        //        Entity.MountPoints.RemoveAt(i);
        //    }

        //    foreach (var mountPointDto in rackDto.MountPoints)
        //    {
        //        if (MountPoints.Contains(mountPointDto))
        //            continue;
        //        // není tam, vytvořit nový...
        //        AddMountPoint(mountPointDto);
        //    }
        //}

        //public string GetSubpath(string fullPath)
        //{
        //    var normalizedFullPath = NormalizePath(fullPath);
        //    foreach (var mountPoint in MountPoints)
        //    {
        //        var normalizedMountPoint = NormalizePath(mountPoint);
        //        if (normalizedFullPath.StartsWith(normalizedMountPoint, StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            if (normalizedFullPath.Length == normalizedMountPoint.Length)
        //                return ".";
        //            return normalizedFullPath.Substring(normalizedMountPoint.Length + 1);
        //        }
        //    }
        //    throw new Exception($"Sorry, the given path {fullPath} does not start with any known mount point");
        //}

        //public static string NormalizePath(string path)
        //{
        //    return Path.GetFullPath(new Uri(path).LocalPath)
        //               .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        //               //.ToUpperInvariant();
        //}
    }
}
