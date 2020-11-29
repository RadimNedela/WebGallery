namespace WebGalery.PictureViewer.Domain
{
    public class Picture
    {
        public Picture(string hash, string fullPath)
        {
            Hash = hash;
            FullPath = fullPath;
        }

        public string Hash { get; set; }
        public string FullPath { get; set; }
    }
}