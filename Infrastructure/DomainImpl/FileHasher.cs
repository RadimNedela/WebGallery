using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Domain.Services.InfrastructureInterfaces;
using Domain.Services.Logging;

namespace Infrastructure.DomainImpl
{
    public class FileHasher : IHasher
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool IsSupportedImage(string path)
        {
            var upper = path.ToUpper();
            return upper.EndsWith(".JPG") || upper.EndsWith(".JPEG");
        }

        public string ComputeFileContentHash(string path)
        {
            Log.Begin($"{nameof(ComputeFileContentHash)}", path);
            Stream stream = GetStream(path);

            var retVal = IsSupportedImage(path) ? ImageHash(stream) : OtherFileHash(stream);

            Log.End($"{nameof(ComputeFileContentHash)}", retVal);
            return retVal;
        }

        private Stream GetStream(string path)
        {
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous);
            return stream;
        }

        public string ComputeStringHash(string theString)
        {
            return ComputeHash(Encoding.UTF8.GetBytes(theString));
        }

        private string OtherFileHash(Stream stream)
        {
            var hash = ComputeHash(stream);
            return hash;
        }

        private string ImageHash(Stream inputStream)
        {
            var image = Image.FromStream(inputStream);
            using MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Bmp);
            var hash = ComputeHash(memoryStream);
            memoryStream.Close();
            return hash;
        }

        private string ComputeHash(Stream stream)
        {
            using SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] hash = sha1.ComputeHash(stream);
            return ConvertHashArrayToString(hash);
        }

        private string ComputeHash(byte[] buffer)
        {
            using SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] hash = sha1.ComputeHash(buffer);
            return ConvertHashArrayToString(hash);
        }

        private string ConvertHashArrayToString(byte[] hash)
        {
            //return Convert.ToBase64String(hash);
            //return System.Text.Encoding.ASCII.GetString(hash);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public string ComputeRandomStringHash(string somePrefix)
        {
            return ComputeStringHash(somePrefix + CreateRandomString(50, 100));
        }

        private string CreateRandomString(int minLength, int maxLength)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789=+-_)(*&^%$#@!`~\\|}{][\"';:/?.>,<";
            int length = random.Next(minLength, maxLength);
            var stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}