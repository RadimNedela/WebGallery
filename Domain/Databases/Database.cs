namespace WebGalery.Domain.Databases
{
    public class Database
    {
        public string Name { get; set; } = null!;
        public string Hash { get; internal set; } = null!;

        protected internal Database() { }
    }
}
