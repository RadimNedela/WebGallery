using WebGalery.Domain.Databases;

namespace WebGalery.Domain.FileServices
{
    internal class FileSystemRootPath : IRootPath
    {
        public const string RootBinderName = ".";

        public string RootPath { get; set; }

        public FileSystemRootPath(IDirectoryReader directoryReader)
        {
            RootPath = NormalizePathSeparators(directoryReader.GetCurrentDirectoryName());
        }

        private string NormalizePathSeparators(string pathString)
        {
            return pathString.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        public IEnumerable<string> NormalizePath(string specificPathString)
        {
            specificPathString = NormalizePathSeparators(specificPathString);

            var retVal = new List<string>();
            if (specificPathString.StartsWith(RootPath))
                specificPathString = specificPathString.Substring(RootPath.Length + 1);
            while (!string.IsNullOrEmpty(specificPathString))
            {
                int index = specificPathString.IndexOf(Path.DirectorySeparatorChar);
                var curDir = specificPathString;
                if (index != -1)
                    curDir = specificPathString.Substring(0, index);
                if (!string.IsNullOrEmpty(curDir))
                    retVal.Add(curDir);

                if (specificPathString.Length > curDir.Length + 1)
                    specificPathString = specificPathString.Substring(curDir.Length + 1);
                else
                    specificPathString = "";
            }
            return retVal;
        }

        public string GetDirectoryName(string path)
        {
            if (Directory.Exists(path))
                return Path.GetFileName(path);
            var dirName = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(dirName))
                dirName = RootBinderName;
            return dirName;
        }
    }
}
