using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Contents.Factories
{
    internal class DisplayableFactory
    {
        private readonly IFileReader fileReader;

        public DisplayableFactory(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        internal IDisplayable CreateFromFile(string file)
        {
            return new Displayable()
            {
                Name = fileReader.GetFileName(file),
            };
        }
    }
}
