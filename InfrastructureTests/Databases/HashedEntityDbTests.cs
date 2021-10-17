using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Infrastructure.Databases;
using WebGalery.Infrastructure.Repositories;
using WebGalery.Infrastructure.Tests.IoC;

namespace WebGalery.Infrastructure.Tests.Databases
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

        Binder _binder;
        Content _content;
        BinderToContent _binderToContent;
        Binder _dirBinder;
        AttributedBinderToContent _dirBinderToContent;

        public const string TestContentElementHash = "ContentEntity01Hash";

        private void AddTestHashedEntityToDb()
        {
            _binder = new Binder
            {
                Hash = "BinderEntity01",
                Label = "Binder entity",
                Type = "Nejaky binder type"
            };
            _content = new Content
            {
                Hash = TestContentElementHash,
                Label = "Content 01",
                Type = "jakysi content type"
            };
            _dirBinder = new Binder
            {
                Hash = "directoryBinder01",
                Label = @"c:\temp",
                Type = "Directory"
            };

            _binderToContent = new BinderToContent
            {
                Binder = _binder,
                Content = _content
            };
            _binder.Contents = new List<BinderToContent> { _binderToContent };
            _content.Binders = new List<BinderToContent> { _binderToContent };

            _dirBinderToContent = new AttributedBinderToContent
            {
                Binder = _dirBinder,
                Content = _content,
                Attribute = "asdf.txt"
            };
            _dirBinder.AttributedContents = new List<AttributedBinderToContent> { _dirBinderToContent };
            _content.AttributedBinders = new List<AttributedBinderToContent> { _dirBinderToContent };

            using var serviceProvider = InfrastructureTestsUtils.ServiceProvider;
            var database = serviceProvider.GetService<IGaleryDatabase>();
            database.Contents.Add(_content);
            database.SaveChanges();
            database.DetachAllEntities();
        }

        private void RemoveTestHashedEntityFromDb()
        {
            using var serviceProvider = InfrastructureTestsUtils.CreateServiceCollection().BuildServiceProvider();
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
            using var serviceProvider = InfrastructureTestsUtils.CreateServiceCollection().BuildServiceProvider();
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