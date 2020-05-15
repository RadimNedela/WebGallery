using Domain;

namespace Application
{
    public interface IDirectoryContentApplication
    {
        string VratCosik();
    }
    public class DirectoryContentApplication : IDirectoryContentApplication
    {
        public string VratCosik()
        {
            DirectoryContent content = new DirectoryContent();
            return content.VratCosik();
        }
    }
}