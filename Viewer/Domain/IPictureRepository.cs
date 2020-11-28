namespace WebGallery.PictureViewer.Domain
{
    public interface IPictureRepository
    {
        bool ContainsHash(string hash);
    }
}