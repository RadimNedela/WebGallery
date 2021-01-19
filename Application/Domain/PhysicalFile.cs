using System.IO;
using WebGalery.Core.DbEntities.Contents;

namespace WebGalery.FileImport.Domain
{
    public class PhysicalFile
    {
        public string Hash { get; set; }
        public ContentTypeEnum Type { get; set; }
        public string FullPath { get; set; }
        public string SubPath { get; set; }

        public static ContentTypeEnum GetContentTypeByExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToUpper();
            switch (extension)
            {
                case ".JPG":
                case ".JPEG":
                    return ContentTypeEnum.Image;
                default:
                    return ContentTypeEnum.Unknown;
            }
        }
    }
}
