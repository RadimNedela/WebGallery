using System;
using System.IO;

namespace Application.Directories
{
    public class PhysicalFileApplication
    {
        public Stream GetStream(string hash)
        {
            var fileName = GetFileName(hash);
            var stream = System.IO.File.OpenRead(fileName);
            return stream;
        }

        private string GetFileName(string hash)
        {
            return "/home/radim/Source/WebGalery/TestPictures/Duha/2017-08-20-Duha0367.JPG";
        }
    }
}