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

        public void AddDisplayable(IList<IDisplayable> displayables, string file)
        {
            var fileName = fileReader.GetFileName(file);
            var hash = hasher.ComputeFileContentHash(file);
            var currentDisplayable = displayables.FirstOrDefault(d => d.Name == fileName);
            if (currentDisplayable != null && currentDisplayable.Hash != hash)
            {
                displayables.Remove(currentDisplayable);
                currentDisplayable = null;
            }
            if (currentDisplayable == null)
            {
                currentDisplayable = new Displayable()
                {
                    Name = fileName,
                    Hash = hash
                };
                displayables.Add(currentDisplayable);
            }
        }
    }
}
