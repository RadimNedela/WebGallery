using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Domain.InfrastructureInterfaces;
using Domain.Logging;

namespace Infrastructure.DomainImpl
{
    public class FileHasher : IHasher
    {
        private static readonly ISimpleLogger log = new Log4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool IsSupportedImage(string path)
        {
            var upper = path.ToUpper();
            return upper.EndsWith(".JPG") || upper.EndsWith(".JPEG");
        }

        public string ComputeFileContentHash(string path)
        {
            log.Begin($"{nameof(ComputeFileContentHash)}.{path}");
            Stream stream = GetStream(path);

            string retVal = null;
            if (IsSupportedImage(path))
                retVal = ImageHash(stream);
            else 
            retVal = OtherFileHash(stream);

            log.End($"{nameof(ComputeFileContentHash)}.{path}");
            return retVal;
        }

        public Stream GetStream(string path)
        {
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous);
            return stream;
        }

        public string ComputeDirectoryHash(string directoryPath)
        {
            return ComputeHash(Encoding.UTF8.GetBytes(directoryPath));
        }

        private string OtherFileHash(Stream stream)
        {
            string hash;
            hash = ComputeHash(stream);
            return hash;
        }

        private string ImageHash(Stream inputStream)
        {
            var image = Image.FromStream(inputStream);
            string hash;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Bmp);
                hash = ComputeHash(memoryStream);
                memoryStream.Close();
            }
            return hash;
        }

        private string ComputeHash(Stream stream)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] hash = sha1.ComputeHash(stream);
                //return Convert.ToBase64String(hash);
                //return System.Text.Encoding.ASCII.GetString(hash);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        private string ComputeHash(byte[] buffer)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                return Convert.ToBase64String(sha1.ComputeHash(buffer));
            }
        }
    }
}