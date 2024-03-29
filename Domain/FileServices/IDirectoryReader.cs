namespace WebGalery.Domain.FileServices
{
    public interface IDirectoryReader
    {
        IEnumerable<string> GetFileNames(string relativePath);

        IEnumerable<string> GetDirectories(string relativePath);

        string GetCurrentDirectoryName();
    }
}