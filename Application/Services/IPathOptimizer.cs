using Domain.Elements.Maintenance;

namespace Domain.Services
{
    public interface IPathOptimizer
    {
        string CreateValidSubpathAccordingToCurrentConfiguration(string fullPath);
        RackElement Rack { get; }
    }
}
