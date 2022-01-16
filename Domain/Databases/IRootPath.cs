namespace WebGalery.Domain.Databases
{
    public interface IRootPath
    {
        string RootPath { get; }

        /// <summary>
        /// Convert the given specific string (for example full name of file in directory structure)
        /// to list of strings that could be used to create Binders (like Directory Binders)
        /// </summary>
        IEnumerable<string> NormalizePath(string specificPathString);
    }
}