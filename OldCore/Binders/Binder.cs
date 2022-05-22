using System.Collections.Generic;
using WebGalery.Core.Maintenance;

namespace WebGalery.Core.DbEntities.Contents
{
    public class Binder : IPersistable
    {
        public const string DirectoryBinderType = "DIRECTORY";

        public string Hash { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }

        public IList<BinderToContent> Contents { get; set; }
        public IList<AttributedBinderToContent> AttributedContents { get; set; }

        public Rack Rack { get; set; }
        public string RackHash { get; set; }
    }
}