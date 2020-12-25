using NUnit.Framework;
using WebGalery.FileImport.Services;

namespace FileImportTests.Domain
{
    [TestFixture]
    public class ContentElementsToDbEntitiesTests
    {
        [Test]
        public void FromDirectory_ToEntity_BasicContentOK()
        {
            var builder = DirectoryContentBuilderTests.CreateContentBuilder();
            var element = builder.GetDirectoryContent(new DirectoryContentThreadInfo { FullPath = DirectoryContentBuilderTests.TestDirectory }).Contents.First();

            var entity = element.ToEntity();

            Assert.That(entity.Hash, Is.EqualTo(element.Hash));
            Assert.That(entity.Label, Is.EqualTo(element.Label));
            Assert.That(entity.Type, Is.EqualTo(element.Type));
        }

        [Test]
        public void FromDirectory_ToEntity_FileNamesOK()
        {
            var builder = DirectoryContentBuilderTests.CreateContentBuilder();

            var binderElement = builder.GetDirectoryContent(new DirectoryContentThreadInfo { FullPath = DirectoryContentBuilderTests.TestDirectory });
            var firstContentElement = binderElement.Contents
                .First(e => e.Label.Contains("0335"));
            var doubledContentElement = binderElement.Contents
                .First(e => e.Label.Contains("0366"));

            var entity1 = firstContentElement.ToEntity();
            var entity2 = doubledContentElement.ToEntity();

            Assert.That(entity1.AttributedBinders.Count, Is.EqualTo(1));
            Assert.That(entity2.AttributedBinders.Count, Is.EqualTo(2));
        }
    }
}
