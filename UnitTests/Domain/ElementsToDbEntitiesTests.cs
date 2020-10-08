using Domain.Elements;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Domain
{
    [TestFixture]
    public class ElementsToDbEntitiesTests
    {
        [Test]
        public void ContentElementFromDirectory_ToEntity_BasicContentOK()
        {
            var builder = DirectoryContentBuilderTests.CreateContentBuilder();
            var element = builder.GetDirectoryContent(DirectoryContentBuilderTests.TestDirectory).Contents.First();

            var entity = element.ToEntity();

            Assert.That(entity.Hash, Is.EqualTo(element.Hash));
            Assert.That(entity.Label, Is.EqualTo(element.Label));
            Assert.That(entity.Type, Is.EqualTo(element.Type));
        }

        [Test]
        public void ContentElementFromDirectory_ToEntity_FileNamesOK()
        {
            var builder = DirectoryContentBuilderTests.CreateContentBuilder();

            var binderElement = builder.GetDirectoryContent(DirectoryContentBuilderTests.TestDirectory);
            var firstContentElement = binderElement.Contents
                .First(e => e.Label.Contains("0335"));
            var doubledContentElement = binderElement.Contents
                .First(e => e.Label.Contains("0366"));

            var entity1 = firstContentElement.ToEntity();
            var entity2 = doubledContentElement.ToEntity();

            Assert.That(entity1.AttributedBinders.Count, Is.EqualTo(1));
            Assert.That(entity2.AttributedBinders.Count, Is.EqualTo(2));
        }

        [Test]
        public void BinderElement_ToEntity_BasicContentOK()
        {
            var builder = DirectoryContentBuilderTests.CreateContentBuilder();
            var binderElement = builder.GetDirectoryContent(DirectoryContentBuilderTests.TestDirectory);

            var binderEntity = binderElement.ToEntity();

            Assert.That(binderEntity.Hash, Is.EqualTo(binderElement.Hash));
            Assert.That(binderEntity.Label, Is.EqualTo(binderElement.Label));
            Assert.That(binderEntity.Type, Is.EqualTo(binderElement.Type));
        }
    }
}
