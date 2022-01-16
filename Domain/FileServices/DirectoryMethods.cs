namespace WebGalery.Domain.FileServices
{
    public class DirectoryMethods : IDirectoryReader
    {
        public string GetCurrentDirectoryName()
        {
            return Directory.GetCurrentDirectory();
        }

        public IEnumerable<string> GetFileNames(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) relativePath = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(relativePath);
            return files;
        }

        public IEnumerable<string> GetDirectories(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) relativePath = Directory.GetCurrentDirectory();
            var directories = Directory.GetDirectories(relativePath);
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
