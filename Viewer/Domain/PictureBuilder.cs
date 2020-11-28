using System;
using System.Linq;

namespace WebGallery.PictureViewer.Domain
{
    public class PictureBuilder
    {
        private readonly IPictureRepository pictureRepository;

        public PictureBuilder(IPictureRepository pictureRepository)
        {
            this.pictureRepository = pictureRepository;
        }

        public Picture Get(string hash)
        {
            var contentEntity = pictureRepository.GetContentEntity(hash);
            if (contentEntity == null)
                throw new Exception($"Entity with hash {hash} not found in the database");
            //var pathBinders = contentEntity.AttributedBinders.Select(ab => ab.)
            return new Picture(hash, "hello");
        }
    }
}
