using WebGalery.Domain.Basics;
using WebGalery.Domain.Warehouses;

namespace WebGalery.Domain.FileServices
{
    public class FileSystemLocation : Entity
    {
        public FileSystemDepot ParentDepot { get; private set; }

        public string LocationString { get; private set; }

        public FileSystemLocation(FileSystemDepot parent, string hash, string locationString)
            : base(hash)
        {
            ParentDepot = ParamAssert.NotNull(parent, nameof(parent));
            LocationString = ParamAssert.NotEmpty(locationString, nameof(locationString));
        }

        private FileSystemLocation(string hash, string locationString)
            : base(hash)
        {
            LocationString = locationString;
        }

        public static string NormalizePath(string pathString)
        {
            return pathString.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        public IEnumerable<string> SplitJourneyToLegs(string specificPathString)
        {
            specificPathString = NormalizePath(specificPathString);

            var retVal = new List<string>();
            if (specificPathString.StartsWith(LocationString))
                specificPathString = specificPathString.Substring(LocationString.Length + 1);
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
