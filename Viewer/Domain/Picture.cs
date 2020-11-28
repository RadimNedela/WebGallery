namespace WebGallery.PictureViewer.Domain
{
    public class Picture
    {
        public Picture(string hash, string path)
        {
            Hash = hash;
            Path = path;
        }

        public string Hash { get; set; }
        public string Path { get; set; }
    }
}