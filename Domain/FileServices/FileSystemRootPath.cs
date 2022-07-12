using WebGalery.Domain.Databases;

namespace WebGalery.Domain.FileServices
{
    internal class FileSystemRootPath : IRootPath
    {
        // public const string RootBinderName = ".";
        public string RootPath { get; private set; }

        public FileSystemRootPath(IDirectoryReader directoryReader)
        {
            RootPath = NormalizePath(directoryReader.GetCurrentDirectoryName());
        }

        private string NormalizePath(string pathString)
        {
            return pathString.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        public IEnumerable<string> SplitPath(string specificPathString)
        {
            specificPathString = NormalizePath(specificPathString);

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
    }
}
