using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Contents.Factories
{
    internal interface IDisplayableFactory
    {
        IDisplayable CreateFromFile(string file);
    }

    internal class DisplayableFactory : IDisplayableFactory
    {
        private readonly IFileReader fileReader;
        private readonly IHasher hasher;

        public DisplayableFactory(IFileReader fileReader, IHasher hasher)
        {
            this.fileReader = fileReader;
            this.hasher = hasher;
        }

        public IDisplayable CreateFromFile(string file)
        {
            return new Displayable()
            {
                Name = fileReader.GetFileName(file),
                Hash = hasher.ComputeFileContentHash(file),
            };
        }
    }
}
