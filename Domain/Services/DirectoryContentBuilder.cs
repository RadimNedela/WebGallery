using Domain.InfrastructureInterfaces;

namespace Domain.Services
{
    public class DirectoryContentBuilder
    {
        private readonly IDirectoryMethods _directoryMethods;

        public DirectoryContentBuilder(IDirectoryMethods directoryMethods)
        {
            _directoryMethods = directoryMethods;
        }

        public string VratCosik()
        {
            return "Cosik z directory content buildru";
        }

    }
}