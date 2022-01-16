using WebGalery.Domain.FileServices;
using WebGalery.Domain.IoC;

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

        public DisplayableFactory(IFileReader? fileReader = null, IHasher? hasher = null)
        {
            this.fileReader = fileReader ?? IoCDefaults.FileReader;
            this.hasher = hasher ?? IoCDefaults.Hasher;
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
