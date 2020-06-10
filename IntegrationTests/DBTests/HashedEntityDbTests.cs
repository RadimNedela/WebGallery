using Domain.Entities;
using Infrastructure.DomainImpl;
using Infrastructure.MySqlDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

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

        // [Test]
        // public void JustCreateTheDatabase()
        // {
        //     using (var context = new TestDbContext())
        //     {
        //         context.Database.EnsureCreated();
        //     }
        // }

        Hashed testElement;
        public const string testElementHash = "TestElementHashValue";
        private void AddTestHashedEntityToDb()
        {
            using (var context = new TestDbContext())
            {
                testElement = new Hashed() { Hash = testElementHash };
                context.Hashed.Add(testElement);
                context.SaveChanges();
                context.DetachAllEntities();
            }
        }

        private void RemoveTestHashedEntityFromDb()
        {
            using (var context = new TestDbContext())
            {
                context.Hashed.Remove(testElement);
                context.SaveChanges();
                context.DetachAllEntities();
            }
        }

        [Test]
        public void GetExistingHashedElement_WillReturnIt()
        {
            AddTestHashedEntityToDb();
            using (var context = new TestDbContext())
            {
                var repo = new HashedEntitiesRepository(context);
                var fromDb = repo.GetHashedEntity(testElementHash);
                Assert.That(fromDb, Is.Not.Null);
                Assert.That(fromDb.Hash, Is.EqualTo(testElementHash));
            }
            RemoveTestHashedEntityFromDb();
        }
    }
}