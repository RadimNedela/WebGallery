namespace WebGalery.Domain.Contents
{
    public class Displayable : IDisplayable
    {
        public string Name { get; internal set; } = null!;

        public string Hash { get; internal set; } = null!;
    }
}