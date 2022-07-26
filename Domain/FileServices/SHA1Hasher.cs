using SixLabors.ImageSharp;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using WebGalery.Domain.Basics;
using WebGalery.Domain.Logging;

namespace WebGalery.Domain.FileServices
{
    public class Sha1Hasher : IHasher
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

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

        public string ComputeDependentStringHash(IEntity parent, string theString)
        {
            return ComputeStringHash(parent.Hash + "_" + theString);
        }

        private string OtherFileHash(Stream stream)
        {
            var hash = ComputeHash(stream);
            return hash;
        }

        private string ImageHash(Stream inputStream)
        {
            string hash;
            using (var image = Image.Load(inputStream))
            {
                using var memoryStream = new MemoryStream();
                image.SaveAsBmp(memoryStream);
                memoryStream.Position = 0;
                hash = ComputeHash(memoryStream);
                memoryStream.Close();
            }
            return hash;
        }

        private string ComputeHash(Stream stream)
        {
            using var sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(stream);
            return ConvertHashArrayToString(hash);
        }

        private string ComputeHash(byte[] buffer)
        {
            using var sha1 = SHA1.Create();
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
            return ComputeStringHash(somePrefix + DateTime.Now.Ticks + CreateRandomString(50, 100));
        }

        public string CreateRandomString(int minLength, int maxLength)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789=+-_)(*&^%$#@!`~\\|}{][\"';:/?.>,<";
            int length = random.Next(minLength, maxLength);
            var stringChars = new char[length];

            for (int i = 0; i < length; i++)
                stringChars[i] = chars[random.Next(chars.Length)];

            var finalString = new string(stringChars);
            return finalString;
        }
    }
}