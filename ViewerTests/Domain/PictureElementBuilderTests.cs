using NSubstitute;
using NUnit.Framework;
using WebGalery.DatabaseEntities;

namespace WebGalery.PictureViewer.Tests.Domain
{
    [TestFixture]
    public class PictureElementBuilderTests
    {
        public const string TestHash2 = "TestHashNr2";

        [Test]
        public void Get_ExistingHash_ReturnsElementWithSameHash()
        {
            var sut = BuildPictureBuilder();

            var element = sut.Get(TestDatabase.Content1Hash);

            Assert.That(element.Hash, Is.EqualTo(TestDatabase.Content1Hash));
        }

        [Test]
        public void Get_NotExistingHash_ThrowsException()
        {
            var builder = BuildPictureBuilder();

            Assert.That(() => builder.Get("NotExisting Hash"), Throws.Exception);
        }

        [TestCase(TestDatabase.Content1Hash, @"/TestPictures/TestPicture.jpg")]
        [TestCase(TestHash2, @"/TestPictures/AnotherTestPicture.jpg")]
        public void Get_Existing_PathIsValid(string hash, string expectedFilePath)
        {
            var builder = BuildPictureBuilder();

            var element = builder.Get(hash);

            Assert.That(element.FullPath, Contains.Substring(expectedFilePath));
        }

        private PictureBuilder BuildPictureBuilder()
        {
            IGaleryReadDatabase database = new TestDatabase();
            IUserInfo userInfo = Substitute.For<IUserInfo>();
            userInfo.IsRackActive(Arg.Any<string>()).Returns(true);
            return new PictureBuilder(userInfo, database);
        }
    }
}
