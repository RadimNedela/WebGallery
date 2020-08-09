using Domain.InfrastructureInterfaces;
using Domain.Services;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Domain
{
    [TestFixture]
    public class DirectoryContentBuilderTests
    {
        [Test]
        public void GetDirectoryContent_ValidPath_ReturnsCorrectNumberOfPictures()
        {
            DirectoryContentBuilder contentBuilder = CreateContentBuilder();

            var content = contentBuilder.GetDirectoryContent(TestDirectory);

            Assert.That(content.Count(), Is.EqualTo(4));
        }

        [Test]
        public void GetDirectoryContent_ValidPath_FirstElementHasCorrectHash()
        {
            DirectoryContentBuilder contentBuilder = CreateContentBuilder();

            var content = contentBuilder.GetDirectoryContent(TestDirectory).First();

            Assert.That(content.Hash, Contains.Substring("2018-01-24-Chopok0335.JPGHash"));
        }

        public const string TestDirectory = "Test directory - chopok";

        private static DirectoryContentBuilder CreateContentBuilder()
        {
            var directoryMethods = Substitute.For<IDirectoryMethods>();
            directoryMethods.GetFileNames(TestDirectory).Returns(new string[] {
                "2018-01-24-Chopok0335.JPG",
                "2018-01-24-Chopok0357.JPG",
                "2018-01-24-Chopok0361.JPG",
                "2018-01-24-Chopok0366.JPG"
            });

            var hasher = Substitute.For<IHasher>();
            hasher.ComputeFileContentHash(Arg.Any<string>()).Returns((ci) => $"Hash{ci.ArgAt<string>(0)}Hash");

            //var hashedEntitiesRepostiory = Substitute.For<IHashedEntitiesRepository>();
            //var hashedElementBuilder = new BinderBuilder(hasher, hashedEntitiesRepostiory);
            return new DirectoryContentBuilder(directoryMethods, hasher);
        }
    }
}