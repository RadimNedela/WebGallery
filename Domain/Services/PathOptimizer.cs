using Domain.Elements.Maintenance;

namespace Domain.Services
{
    public class PathOptimizer : IPathOptimizer
    {
        private readonly IDatabaseInfoProvider _databaseInfoProvider;

        public PathOptimizer(IDatabaseInfoProvider databaseInfoProvider)
        {
            _databaseInfoProvider = databaseInfoProvider;
        }

        public RackElement Rack => _databaseInfoProvider.CurrentRack;

        public string CreateValidSubpathAccordingToCurrentConfiguration(string fullPath)
        {
            return Rack.GetSubpath(fullPath);
        }
    }
}
