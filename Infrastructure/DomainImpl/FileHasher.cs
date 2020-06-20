using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Domain.InfrastructureInterfaces;

namespace Infrastructure.DomainImpl
{
    public class FileHasher : IHasher
    {
        public bool IsSupportedImage(string path)
        {
            var upper = path.ToUpper();
            return upper.EndsWith(".JPG") || upper.EndsWith(".JPEG");
        }

        public string ComputeFileContentHash(string path)
        {
            if (IsSupportedImage(path))
                return ImageHash(path);
            return OtherFileHash(path);
        }

        public string ComputeDirectoryHash(string directoryPath)
        {
            return ComputeHash(Encoding.UTF8.GetBytes(directoryPath));
        }

        private string OtherFileHash(string path)
        {
            string hash;
            using (var stream = new FileStream(path, FileMode.Open))
            {
                hash = ComputeHash(stream);
            }
            return hash;
        }

        private string ImageHash(string path)
        {
            var image = Image.FromFile(path);
            string hash;
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Bmp);
                stream.Close();
                hash = ComputeHash(stream);
            }
            return hash;
        }

        private string ComputeHash(Stream stream)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                return Convert.ToBase64String(sha1.ComputeHash(stream));
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