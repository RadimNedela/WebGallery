using WebGallery.PictureViewer.Domain;

namespace Application.Directories
{
    public class BinderApp
    {
        private readonly PictureBuilder _contentElementBuilder;

        public BinderApp(
            PictureBuilder contentElementBuilder)
        {
            _contentElementBuilder = contentElementBuilder;
        }

        //public ContentInfoDto GetContent(string hash)
        //{
        //    var element = _elementsMemoryStorage.Get(hash);
        //    if (element == null)
        //    {
        //        // get it somehow from database 
        //        element = _contentElementBuilder.Get(hash);
        //        _elementsMemoryStorage.Add(element);
        //    }
        //    return element.ToContentInfoDto();
        //}
    }
}