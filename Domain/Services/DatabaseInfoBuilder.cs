using Domain.DbEntities.Maintenance;
using Domain.InfrastructureInterfaces;
using System;

namespace Domain.Services
{
    public class DatabaseInfoBuilder
    {
        private IHasher hasher;

        public DatabaseInfoBuilder(IHasher hasher)
        {
            this.hasher = hasher;
        }

        public DatabaseInfoEntity BuildNewDatabase(string databaseName)
        {
            string newHash = hasher.ComputeStringHash(databaseName + CreateRandomString(50, 100));

            var infoEntity = new DatabaseInfoEntity()
            {
                Name = databaseName,
                Hash = newHash,
            };

            return infoEntity;
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
