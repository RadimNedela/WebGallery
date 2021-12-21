namespace WebGalery.Domain.Contents.Factories
{
    internal class DisplayableFactory
    {
        internal IDisplayable CreateFromFile(string file)
        {
            return new Displayable();
        }
    }
}
