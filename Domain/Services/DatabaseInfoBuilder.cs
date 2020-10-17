using Domain.Elements.Maintenance;
using Domain.InfrastructureInterfaces;
using System;

namespace Domain.Services
{
    public class DatabaseInfoBuilder
    {
        private IHasher hasher;
        private readonly IDatabaseInfoEntityRepository repository;

        public DatabaseInfoBuilder(IHasher hasher, IDatabaseInfoEntityRepository repository)
        {
            this.hasher = hasher;
            this.repository = repository;
        }

        public DatabaseInfoElement GetDatabase(string hash)
        {
            var entity = repository.Get(hash);
            var element = new DatabaseInfoElement(hasher, entity);
            return element;
        }

        public DatabaseInfoElement BuildNewDatabase(string databaseName)
        {
            string newHash = hasher.ComputeStringHash(databaseName + CreateRandomString(50, 100));

            var infoElement = new DatabaseInfoElement(hasher, databaseName, newHash);

            return infoElement;
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
