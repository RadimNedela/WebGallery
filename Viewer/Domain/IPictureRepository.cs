using Domain.DbEntities;

namespace WebGallery.PictureViewer.Domain
{
    public interface IPictureRepository
    {
        ContentEntity GetContentEntity(string hash);
    }
}