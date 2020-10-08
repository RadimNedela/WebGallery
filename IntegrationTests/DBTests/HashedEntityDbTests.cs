using Domain.DbEntities;
using Domain.Logging;
using Infrastructure.DomainImpl;
using Infrastructure.MySqlDb;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class HashedEntityDbTests
    {
        private static readonly ISimpleLogger log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        BinderEntity binder;
        ContentEntity content;
        BinderEntityToContentEntity binderToContent;
        AttributedBinderEntity dirBinder;
        AttributedBinderEntityToContentEntity dirBinderToContent;

        public const string TestContentElementHash = "ContentEntity01Hash";

        private void AddTestHashedEntityToDb()
        {
            binder = new BinderEntity
            {
                Hash = "BinderEntity01",
                Label = "Binder entity",
                Type = "Nejaky binder type"
            };
            content = new ContentEntity
            {
                Hash = TestContentElementHash,
                Label = "Content 01",
                Type = "jakysi content type"
            };
            dirBinder = new AttributedBinderEntity
            {
                Hash = "directoryBinder01",
                Label = @"c:\temp",
                Type = "Directory"
            };

            binderToContent = new BinderEntityToContentEntity
            {
                Binder = binder,
                Content = content
            };
            binder.Contents = new List<BinderEntityToContentEntity> { binderToContent };
            content.Binders = new List<BinderEntityToContentEntity> { binderToContent };

            dirBinderToContent = new AttributedBinderEntityToContentEntity
            {
                AttributedBinder = dirBinder,
                Content = content,
                Attribute = "asdf.txt"
            };
            dirBinder.AttributedContents = new List<AttributedBinderEntityToContentEntity> { dirBinderToContent };
            content.AttributedBinders = new List<AttributedBinderEntityToContentEntity> { dirBinderToContent };

            using var context = new MySqlDbContext();
            context.Contents.Add(content);
            context.SaveChanges();
            context.DetachAllEntities();
        }

        private void RemoveTestHashedEntityFromDb()
        {
            using var context = new MySqlDbContext();
            context.Binders.Remove(binder);
            context.Contents.Remove(content);
            context.AttributedBinders.Remove(dirBinder);
            context.SaveChanges();
            context.DetachAllEntities();
        }

        [Test]
        public void GetExistingHashedElement_WillReturnIt()
        {
            using var context = new MySqlDbContext();
            var repo = new ContentEntitiesRepository(context);
            var fromDb = repo.Get(TestContentElementHash);
            Assert.That(fromDb, Is.Not.Null, "Returned element is null");
            Assert.That(fromDb.Hash, Is.EqualTo(TestContentElementHash), "Hash is not equal");
            Assert.That(fromDb.Binders, Is.Not.Null, "Binders is null");
            Assert.That(fromDb.AttributedBinders, Is.Not.Null, "AttributedBinders is null");
            var binder = fromDb.Binders.First().Binder;
            Assert.That(binder, Is.Not.Null, "First Binder is null");
            Assert.That(binder.Label, Is.Not.Empty, "Binder Lable is empty");
            var dirBinderHolder = fromDb.AttributedBinders.First();
            Assert.That(dirBinderHolder, Is.Not.Null, "First Dir Binder is null");
            Assert.That(dirBinderHolder.Attribute, Is.Not.Empty, "Dir binder attribute is empty");
            var dirBinder = dirBinderHolder.AttributedBinder;
            Assert.That(dirBinder, Is.Not.Null, "Dir Binder is null");
            Assert.That(dirBinder.Label, Is.Not.Empty, "Dir binder label is empty");
        }
    }
}