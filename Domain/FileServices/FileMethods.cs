namespace WebGalery.Domain.FileServices
{
    internal class FileMethods : IFileReader
    {
        public string GetFileName(string file)
        {
            var fileName = Path.GetFileName(file);
            return fileName;
        }
    }
}
