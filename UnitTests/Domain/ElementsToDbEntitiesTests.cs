using Domain.Elements;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Domain
{
    [TestFixture]
    public class ElementsToDbEntitiesTests
    {
        [Test]
        public void ContentElement_ToEntity_ContentOK()
        {
            var builder = DirectoryContentBuilderTests.CreateContentBuilder();
            var element = builder.GetDirectoryContent(DirectoryContentBuilderTests.TestDirectory).Contents.First();

            var entity = element.ToEntity();

            Assert.That(entity.Hash, Is.EqualTo(element.Hash));
            Assert.That(entity.Label, Is.EqualTo(element.Label));
            Assert.That(entity.Type, Is.EqualTo(element.Type));
        }

        [Test]
        public void BinderElement_ToEntity_ContentOK()
        {
            var builder = DirectoryContentBuilderTests.CreateContentBuilder();
            var element = builder.GetDirectoryContent(DirectoryContentBuilderTests.TestDirectory);

            var entity = element.ToEntity();

            Assert.That(entity.Hash, Is.EqualTo(element.Hash));
            Assert.That(entity.Label, Is.EqualTo(element.Label));
            Assert.That(entity.Type, Is.EqualTo(element.Type));
        }

        [Test]
        public void BinderElement_ToEntity_AllContentsExisting()
        {
            var builder = DirectoryContentBuilderTests.CreateContentBuilder();
            var element = builder.GetDirectoryContent(DirectoryContentBuilderTests.TestDirectory);

            var entity = element.ToEntity();

            Assert.That(entity.Contents, Is.Not.Null);
            Assert.That(entity.Contents.Count, Is.EqualTo(4));
        }
    }
}
