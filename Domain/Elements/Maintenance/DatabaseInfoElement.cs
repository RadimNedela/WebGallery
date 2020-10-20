using Domain.DbEntities.Maintenance;
using Domain.InfrastructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Elements.Maintenance
{
    public class DatabaseInfoElement
    {
        private readonly IHasher hasher;

        public string Hash { get; private set; }
        public string Name { get; private set; }
        public IList<RackElement> Racks { get; private set; }

        public DatabaseInfoEntity Entity { get; }

        public DatabaseInfoElement(IHasher hasher, DatabaseInfoEntity databaseInfoEntity)
        {
            this.hasher = hasher;
            Entity = databaseInfoEntity;
            Hash = Entity.Hash;
            Name = Entity.Name;
            Racks = Entity.Racks.Select(re => new RackElement(hasher, re)).ToList();
        }

        public DatabaseInfoElement(IHasher hasher, string databaseName)
        {
            this.hasher = hasher;

            Name = databaseName;
            Hash = hasher.ComputeStringHash(databaseName + CreateRandomString(50, 100));
            Racks = new List<RackElement>();

            Entity = new DatabaseInfoEntity
            {
                Name = Name,
                Hash = Hash,
                Racks = new List<RackEntity>(),
            };

            AddNewRack("Default", "");
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


        public void AddNewRack(string name, string initialMountPointPath)
        {
            if (Racks.Any(r => r.Name == name))
                throw new NotSupportedException("Cannot add new rack with already existing name, the name must be unique in the database");

            var rack = new RackElement(Entity, hasher, name, initialMountPointPath);
            Racks.Add(rack);
            Entity.Racks.Add(rack.Entity);
        }
    }
}
