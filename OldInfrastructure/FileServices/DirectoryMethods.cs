﻿using System;
using System.Collections.Generic;
using System.IO;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Infrastructure.FileServices
{
    public class DirectoryMethods : IDirectoryMethods
    {
        public string GetCurrentDirectoryName()
        {
            return Directory.GetCurrentDirectory();
        }

        public IEnumerable<string> GetFileNames(string directoryName)
        {
            if (string.IsNullOrWhiteSpace(directoryName)) directoryName = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(directoryName);
            return files;
        }

        public IEnumerable<string> GetDirectories(string directoryName)
        {
            if (string.IsNullOrWhiteSpace(directoryName)) directoryName = Directory.GetCurrentDirectory();
            var directories = Directory.GetDirectories(directoryName);
            return directories;
        }

        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            //.ToUpperInvariant();
        }
    }
}
