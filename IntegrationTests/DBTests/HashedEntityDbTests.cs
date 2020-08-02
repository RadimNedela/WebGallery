using Domain.DbEntities;
using Infrastructure.DomainImpl;
using Infrastructure.MySqlDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class HashedEntityDbTests
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public class TestDbContext : MySqlDbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder
                   .UseLoggerFactory(MyLoggerFactory); // Warning: Do not create a new ILoggerFactory instance each time

                base.OnConfiguring(optionsBuilder);
            }
        }

        [SetUp]
        public void OneTimeSetUp()
        {
            AddTestHashedEntityToDb();
        }

        [TearDown]
        public void OneTimeTearDown()
        {
            RemoveTestHashedEntityFromDb();
        }

        HashedEntity hashed;
        LocationEntity location;
        LocationTagEntity locationTag;
        HashedTagEntity hashedTag;

        public const string testElementHash = "TestElementHashValue";
        private void AddTestHashedEntityToDb()
        {
            locationTag = new LocationTagEntity()
            {
                Description = "Test Location Tag",
                Name = "Test Location Name",
                Value = 2
            };
            location = new LocationEntity()
            {
                Directory = "Test Directory",
                FileName = "Test File Name"
            };

            LocationEntityToTagEntity locationToTag = new LocationEntityToTagEntity()
            {
                Location = location,
                Tag = locationTag
            };
            location.Tags = new List<LocationEntityToTagEntity>() { locationToTag };

            hashed = new HashedEntity()
            {
                Hash = testElementHash,
                Type = "TestHashType"
            };

            LocationEntityToHashedEntity locationToHashed = new LocationEntityToHashedEntity()
            {
                Location = location,
                Hashed = hashed
            };

            hashed.Locations = new List<LocationEntityToHashedEntity> { locationToHashed };

            hashedTag = new HashedTagEntity()
            {
                Description = "Test Hashed Tag Description",
                Name = "Test Hashed Tag Name"
            };

            HashedEntityToTagEntity hashedToTag = new HashedEntityToTagEntity()
            {
                Hashed = hashed,
                Tag = hashedTag
            };

            hashed.Tags = new List<HashedEntityToTagEntity> { hashedToTag };

            using var context = new TestDbContext();
            context.Hashes.Add(hashed);
            context.SaveChanges();
            context.DetachAllEntities();
        }

        private void RemoveTestHashedEntityFromDb()
        {
            using (var context = new TestDbContext())
            {
                context.Hashes.Remove(hashed);
                context.Locations.Remove(location);
                context.Tags.Remove(locationTag);
                context.Tags.Remove(hashedTag);
                context.SaveChanges();
                context.DetachAllEntities();
            }
        }

        [Test]
        public void GetExistingHashedElement_WillReturnIt()
        {
            using (var context = new TestDbContext())
            {
                var repo = new HashedEntitiesRepository(context);
                HashedEntity fromDb = repo.Get(testElementHash);
                Assert.That(fromDb, Is.Not.Null, "Returned element is null");
                Assert.That(fromDb.Hash, Is.EqualTo(testElementHash), "Hash is not equal");
                Assert.That(fromDb.Locations, Is.Not.Null, "Locations is null");
                Assert.That(fromDb.Tags, Is.Not.Null, "Tags is null");
                var location = fromDb.Locations.First().Location;
                Assert.That(location, Is.Not.Null, "First Location is null");
                Assert.That(location.Tags, Is.Not.Null, "Location tags is null");
                var locationTag = location.Tags.FirstOrDefault();
                Assert.That(locationTag, Is.Not.Null, "location Tag is null");
                Assert.That(locationTag.Tag, Is.Not.Null, "location tag TAG is null");
                Assert.That(locationTag.Tag.Description, Is.EqualTo("Test Location Tag"));
            }
        }
    }
}