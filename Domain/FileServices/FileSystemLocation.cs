using WebGalery.Domain.Warehouses;

namespace WebGalery.Domain.FileServices
{
    internal class FileSystemLocation : ILocation
    {
        public string Name { get; private set; }

        public FileSystemLocation(IDirectoryReader directoryReader)
        {
            Name = NormalizePath(directoryReader.GetCurrentDirectoryName());
        }

        public FileSystemLocation(string name)
        {
            Name = name;
        }

        private string NormalizePath(string pathString)
        {
            return pathString.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        public IEnumerable<string> SplitJourneyToLegs(string specificPathString)
        {
            specificPathString = NormalizePath(specificPathString);

            var retVal = new List<string>();
            if (specificPathString.StartsWith(Name))
                specificPathString = specificPathString.Substring(Name.Length + 1);
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
