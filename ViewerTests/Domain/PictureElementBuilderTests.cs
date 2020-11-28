using NSubstitute;
using NUnit.Framework;
using WebGallery.PictureViewer.Domain;

namespace WebGallery.PictureViewer.Tests.Domain
{
    [TestFixture]
    public class PictureElementBuilderTests
    {
        public const string TestHash1 = "TestHash";
        public const string TestHash2 = "TestHashNr2";

        [Test]
        public void Get_ExistingHash_ReturnsElementWithSameHash()
        {
            var sut = BuildPictureBuilder();

            var element = sut.Get(TestHash1);

            Assert.That(element.Hash, Is.EqualTo(TestHash1));
        }

        [Test]
        public void Get_NotExistingHash_ThrowsException()
        {
            var builder = BuildPictureBuilder();

            Assert.That(() => builder.Get("NotExisting Hash"), Throws.Exception);
        }

        [TestCase(TestHash1, @"/TestPictures/TestPicture.jpg")]
        [TestCase(TestHash2, @"/TestPictures/AnotherTestPicture.jpg")]
        public void Get_Existing_PathIsValid(string hash, string expectedFilePath)
        {
            var builder = BuildPictureBuilder();

            var element = builder.Get(hash);

            Assert.That(element.Path, Contains.Substring(expectedFilePath));
        }

        private PictureBuilder BuildPictureBuilder()
        {
            var pictureRepository = Substitute.For<IPictureRepository>();
            pictureRepository.ContainsHash("TestHash").Returns(true);
            return new PictureBuilder(pictureRepository);
        }
    }
}
