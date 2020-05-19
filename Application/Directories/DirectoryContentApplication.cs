using Domain.Services;

namespace Application.Directories
{
    public class DirectoryContentApplication : IDirectoryContentApplication
    {
        private readonly DirectoryContentBuilder _directoryContentBuilder;

        public DirectoryContentApplication(DirectoryContentBuilder directoryContentBuilder)
        {
            _directoryContentBuilder = directoryContentBuilder;
        }

        public string VratCosik()
        {
            return _directoryContentBuilder.VratCosik();
        }
    }
}