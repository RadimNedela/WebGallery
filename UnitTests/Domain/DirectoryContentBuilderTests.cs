using NSubstitute;
using NUnit.Framework;
using System.Linq;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.FileImport.Domain;
using WebGalery.Maintenance.Domain;

namespace FileImportTests.Domain
{
    [TestFixture]
    public class DirectoryContentBuilderTests
    {
        [Test]
        public void GetDirectoryContent_ValidPath_ReturnsCorrectNumberOfPictures()
        {
            PhysicalFilesParser contentBuilder = CreateContentBuilder();

            var files = contentBuilder.ParsePhysicalFiles(new DirectoryContentThreadInfo { FullPath = TestDirectory });

            Assert.That(files.Count(), Is.EqualTo(5));
        }

        [Test]
        public void GetDirectoryContent_ValidPath_FirstElementHasCorrectHash()
        {
            PhysicalFilesParser contentBuilder = CreateContentBuilder();

            var content = contentBuilder.ParsePhysicalFiles(new DirectoryContentThreadInfo { FullPath = TestDirectory }).First();

            Assert.That(content.Hash, Contains.Substring("2018-01-24-Chopok0335.JPGHash"));
        }

        [Test]
        public void JpgElement_IsImageType()
        {
            PhysicalFilesParser contentBuilder = CreateContentBuilder();

            var content = contentBuilder.ParsePhysicalFiles(new DirectoryContentThreadInfo { FullPath = TestDirectory }).First();

            Assert.That(content.Type, Is.EqualTo(ContentTypeEnum.Image));
        }

        [Test]
        public void ParsingFiles_ChangesFilesAndFilesDoneCounts()
        {
            PhysicalFilesParser contentBuilder = CreateContentBuilder();

            var info = new DirectoryContentThreadInfo { FullPath = TestDirectory };
            contentBuilder.ParsePhysicalFiles(info).First();

            Assert.That(info.Files, Is.EqualTo(5));
            Assert.That(info.FilesDone, Is.EqualTo(1));

            contentBuilder.ParsePhysicalFiles(info).Last();
            Assert.That(info.Files, Is.EqualTo(5));
            Assert.That(info.FilesDone, Is.EqualTo(5));
        }

        public const string TestDirectory = "Test directory - chopok";

        public static PhysicalFilesParser CreateContentBuilder()
        {
            var directoryMethods = Substitute.For<IDirectoryMethods>();
            directoryMethods.GetFileNames(TestDirectory).Returns(new[] {
                        "2018-01-24-Chopok0335.JPG",
                        "2018-01-24-Chopok0357.JPG",
                        "2018-01-24-Chopok0361.JPG",
                        "2018-01-24-Chopok0366.JPG",
                        "2018-01-24-Chopok0366ASDF.JPG"
                    });

            var hasher = Substitute.For<IHasher>();
            hasher.ComputeFileContentHash(Arg.Any<string>()).Returns(ci => $"Hash{ci.ArgAt<string>(0)}Hash".Replace("ASDF", ""));
            CurrentDatabaseInfoProvider cdiProvider = new CurrentDatabaseInfoProvider(null, null);

            return new PhysicalFilesParser(directoryMethods, hasher, cdiProvider);
        }
    }
}