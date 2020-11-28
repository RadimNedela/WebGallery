using System;

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
            if (pictureRepository.ContainsHash(hash))
            return new Picture(hash, "hello");
            throw new NotImplementedException();
        }
    }
}
