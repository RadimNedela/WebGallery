using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using Domain.InfrastructureInterfaces;

namespace Infrastructure.DomainImpl
{
    public class FileHasher : IHasher
    {
        public string GetImageHash(string path)
        {
            var image = Image.FromFile(path);
            string hash;
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Bmp);
                stream.Close();
                byte[] byteArray = stream.ToArray();
                using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                {
                    hash = Convert.ToBase64String(sha1.ComputeHash(byteArray));
                }
            }
            return hash;
        }

    }
}