using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Contents.Factories
{
    internal class DisplayableFactory
    {
        private readonly IFileReader fileReader;
        private readonly IHasher hasher;

        public DisplayableFactory(IFileReader fileReader, IHasher hasher)
        {
            this.fileReader = fileReader;
            this.hasher = hasher;
        }

        internal IDisplayable CreateFromFile(string file)
        {
            return new Displayable()
            {
                Name = fileReader.GetFileName(file),
                Hash = hasher.ComputeFileContentHash(file),
            };
        }
    }
}
