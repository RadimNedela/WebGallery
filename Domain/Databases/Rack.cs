using System.Collections.Generic;

namespace WebGalery.Domain.Databases
{
    public class Rack
    {
        public IList<IRootPath> RootPaths { get; set; } = new List<IRootPath>();

        public IRootPath ActivePath => RootPaths.First();
    }
}
