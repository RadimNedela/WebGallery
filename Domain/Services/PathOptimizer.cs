using Domain.Elements.Maintenance;
using System.Linq;

namespace Domain.Services
{
    public class PathOptimizer : IPathOptimizer
    {
        private readonly IDatabaseInfoProvider databaseInfoProvider;

        public PathOptimizer(IDatabaseInfoProvider databaseInfoProvider)
        {
            this.databaseInfoProvider = databaseInfoProvider;
        }

        public RackElement Rack => databaseInfoProvider.CurrentRack;

        public string CreateValidSubpathAccordingToCurrentConfiguration(string fullPath)
        {
            return Rack.GetSubpath(fullPath);
        }
    }
}
