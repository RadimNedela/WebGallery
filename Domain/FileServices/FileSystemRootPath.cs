using WebGalery.Domain.Databases;

namespace WebGalery.Domain.FileServices
{
    internal class FileSystemRootPath : IRootPath
    {
        public const string RootBinderName = ".";

        public string RootPath { get; set; }

        public FileSystemRootPath(IDirectoryReader directoryReader)
        {
            RootPath = directoryReader.GetCurrentDirectoryName();
        }

        public IEnumerable<string> NormalizePath(string specificPathString)
        {
            var retVal = new List<string>();
            if (specificPathString.StartsWith(RootPath))
                specificPathString = specificPathString.Substring(RootPath.Length);
            string subPath;
            do
            {
                subPath = GetDirectoryName(specificPathString);
                retVal.Prepend(subPath);
                specificPathString = specificPathString.Substring(0, specificPathString.Length - subPath.Length);
            } while (subPath != RootBinderName);
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
