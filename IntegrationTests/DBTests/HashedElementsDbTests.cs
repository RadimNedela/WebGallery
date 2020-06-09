using Domain.Elements;
using Infrastructure.DomainImpl;
using Infrastructure.MySqlDb;
using IntegrationTests.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class HashedElementsDbTests
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
        // public void FirstOne()
        // {
        //     using (var context = new TestDbContext())
        //     {
        //         context.Database.EnsureCreated();
        //         var element = new HashedElement() { Hash = "abcdefghijklmno" };
        //         context.HashedElements.Add(element);
        //         context.SaveChanges();
        //     }
        // }

        HashedElement testElement;

        private void AddTestHashedElementToDb()
        {
            using (var context = new TestDbContext())
            {
                testElement = new HashedElement() { Hash = "TestHashToAddToDB" };
                context.HashedElements.Add(testElement);
                context.SaveChanges();
                context.DetachAllEntities();
            }
        }

        private void RemoveTestHashedElementFromDb()
        {
            using (var context = new TestDbContext())
            {
                context.HashedElements.Remove(testElement);
                context.SaveChanges();
                context.DetachAllEntities();
            }
        }

        [Test]
        public void GetExistingHashedElement_WillReturnIt()
        {
            AddTestHashedElementToDb();
            using (var context = new TestDbContext())
            {
                var repo = new HashedElementsRepository(context);
                var fromDb = repo.GetHashedElement("TestHashToAddToDB");
                Assert.That(fromDb, Is.Not.Null);
                Assert.That(fromDb.Hash, Is.EqualTo("TestHashToAddToDB"));
            }
            RemoveTestHashedElementFromDb();
        }
    }
}