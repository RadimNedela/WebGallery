namespace WebGalery.Domain.FileServices
{
    public class FileSystemLocationFactory
    {
        private readonly IDirectoryReader _directoryReader;

        public FileSystemLocationFactory(IDirectoryReader directoryReader)
        {
            _directoryReader = directoryReader;
        }

        public FileSystemLocation CreateDefault()
        {
            return new FileSystemLocation(_directoryReader);
        }
    }
}