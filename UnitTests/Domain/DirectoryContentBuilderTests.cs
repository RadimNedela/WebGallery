using Domain.Elements;
using Domain.Elements.Maintenance;
using Domain.InfrastructureInterfaces;
using Domain.Services;
using NSubstitute;
using NUnit.Framework;
using System.IO;
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

            var binder = contentBuilder.GetDirectoryContent(TestDirectory);

            Assert.That(binder.Contents.Count(), Is.EqualTo(4));
        }

        [Test]
        public void GetDirectoryContent_ValidPath_FirstElementHasCorrectHash()
        {
            DirectoryContentBuilder contentBuilder = CreateContentBuilder();

            var content = contentBuilder.GetDirectoryContent(TestDirectory).Contents.First();

            Assert.That(content.Hash, Contains.Substring("2018-01-24-Chopok0335.JPGHash"));
        }

        [Test]
        public void JpgElement_IsImageType()
        {
            DirectoryContentBuilder contentBuilder = CreateContentBuilder();

            var content = contentBuilder.GetDirectoryContent(TestDirectory).Contents.First();

            Assert.That(content.Type, Is.EqualTo(ContentElement.ImageType));
        }

        public const string TestDirectory = "Test directory - chopok";

        public static DirectoryContentBuilder CreateContentBuilder()
        {
            var directoryMethods = Substitute.For<IDirectoryMethods>();
            directoryMethods.GetFileNames(TestDirectory).Returns(new string[] {
                "2018-01-24-Chopok0335.JPG",
                "2018-01-24-Chopok0357.JPG",
                "2018-01-24-Chopok0361.JPG",
                "2018-01-24-Chopok0366.JPG",
                "2018-01-24-Chopok0366ASDF.JPG"
            });

            var hasher = Substitute.For<IHasher>();
            hasher.ComputeFileContentHash(Arg.Any<string>()).Returns((ci) => $"Hash{ci.ArgAt<string>(0)}Hash".Replace("ASDF", ""));

            var dip = Substitute.For<IPathOptimizer>();
            dip.CreateValidSubpathAccordingToCurrentConfiguration(Arg.Any<string>()).Returns(i => i.ArgAt<string>(0));

            return new DirectoryContentBuilder(directoryMethods, hasher, new ElementsMemoryStorage(), dip);
        }
    }
}