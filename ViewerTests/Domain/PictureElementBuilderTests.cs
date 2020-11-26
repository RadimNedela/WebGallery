using NUnit.Framework;
using WebGallery.PictureViewer.Domain;

namespace WebGallery.PictureViewerTests.Domain
{
    [TestFixture]
    public class PictureElementBuilderTests
    {
        [Test]
        public void Get_UsingExistingHash_ReturnsElementWithSameHash()
        {
            //var repository = Substitute.For<IContentEntityRepository>();
            var sut = new PictureBuilder(/*repository*/);

            var element = sut.Get("TestHash");

            Assert.That(element.Hash, Is.EqualTo("TestHash"));
        }
    }
}
