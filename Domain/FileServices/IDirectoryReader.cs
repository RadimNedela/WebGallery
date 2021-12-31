namespace WebGalery.Domain.FileServices
{
    public interface IDirectoryReader
    {
        IEnumerable<string> GetFileNames(string directoryName);

        IEnumerable<string> GetDirectories(string directoryName);

        string GetDirectoryName(string directoryName);
    }
}