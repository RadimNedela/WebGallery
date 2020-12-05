using System.Collections.Generic;
using System.Linq;
using Domain.DbEntities;
using Infrastructure.Databases;
using Infrastructure.DomainImpl;
using IntegrationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class HashedEntityDbTests
    {
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

        BinderEntity _binder;
        ContentEntity _content;
        BinderEntityToContentEntity _binderToContent;
        BinderEntity _dirBinder;
        AttributedBinderEntityToContentEntity _dirBinderToContent;

        public const string TestContentElementHash = "ContentEntity01Hash";

        private void AddTestHashedEntityToDb()
        {
            _binder = new BinderEntity
            {
                Hash = "BinderEntity01",
                Label = "Binder entity",
                Type = "Nejaky binder type"
            };
            _content = new ContentEntity
            {
                Hash = TestContentElementHash,
                Label = "Content 01",
                Type = "jakysi content type"
            };
            _dirBinder = new BinderEntity
            {
                Hash = "directoryBinder01",
                Label = @"c:\temp",
                Type = "Directory"
            };

            _binderToContent = new BinderEntityToContentEntity
            {
                Binder = _binder,
                Content = _content
            };
            _binder.Contents = new List<BinderEntityToContentEntity> { _binderToContent };
            _content.Binders = new List<BinderEntityToContentEntity> { _binderToContent };

            _dirBinderToContent = new AttributedBinderEntityToContentEntity
            {
                Binder = _dirBinder,
                Content = _content,
                Attribute = "asdf.txt"
            };
            _dirBinder.AttributedContents = new List<AttributedBinderEntityToContentEntity> { _dirBinderToContent };
            _content.AttributedBinders = new List<AttributedBinderEntityToContentEntity> { _dirBinderToContent };

            using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
            var database = serviceProvider.GetService<IGaleryDatabase>();
            database.Contents.Add(_content);
            database.SaveChanges();
            database.DetachAllEntities();
        }

        private void RemoveTestHashedEntityFromDb()
        {
            using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
            var database = serviceProvider.GetService<IGaleryDatabase>();
            database.Binders.Remove(_binder);
            database.Contents.Remove(_content);
            database.Binders.Remove(_dirBinder);
            database.SaveChanges();
            database.DetachAllEntities();
        }

        [Test]
        public void GetExistingHashedElement_WillReturnIt()
        {
            using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
            var database = serviceProvider.GetService<IGaleryDatabase>();
            var repo = new ContentEntitiesRepository(database);
            var fromDb = repo.Get(TestContentElementHash);
            Assert.That(fromDb, Is.Not.Null, "Returned element is null");
            Assert.That(fromDb.Hash.Trim(), Is.EqualTo(TestContentElementHash), "Hash is not equal");
            Assert.That(fromDb.Binders, Is.Not.Null, "Binders is null");
            Assert.That(fromDb.AttributedBinders, Is.Not.Null, "AttributedBinders is null");
            var binder = fromDb.Binders.First().Binder;
            Assert.That(binder, Is.Not.Null, "First Binder is null");
            Assert.That(binder.Label, Is.Not.Empty, "Binder Lable is empty");
            var dirBinderHolder = fromDb.AttributedBinders.First();
            Assert.That(dirBinderHolder, Is.Not.Null, "First Dir Binder is null");
            Assert.That(dirBinderHolder.Attribute, Is.Not.Empty, "Dir binder attribute is empty");
            var dirBinder = dirBinderHolder.Binder;
            Assert.That(dirBinder, Is.Not.Null, "Dir Binder is null");
            Assert.That(dirBinder.Label, Is.Not.Empty, "Dir binder label is empty");
        }
    }
}