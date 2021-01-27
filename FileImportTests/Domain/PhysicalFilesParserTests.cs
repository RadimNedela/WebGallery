using NUnit.Framework;
using System.Linq;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.Tests;
using WebGalery.FileImport.Domain;

namespace FileImportTests.Domain
{
    [TestFixture]
    public class PhysicalFilesParserTests
    {
        [Test]
        public void ParsePhysicalFiles_ValidPath_ReturnsCorrectNumberOfPictures()
        {
            var physicalFilesParser = new FileImportTestData().CreateTestPhysicalFilesParser();

            var files = physicalFilesParser.ParsePhysicalFiles(CreateThreadInfo());

            Assert.That(files.Count(), Is.EqualTo(5));
        }

        [Test]
        public void ParsePhysicalFiles_ValidPath_FirstElementHasCorrectHash()
        {
            var physicalFilesParser = new FileImportTestData().CreateTestPhysicalFilesParser();

            var content = physicalFilesParser.ParsePhysicalFiles(CreateThreadInfo()).First();

            Assert.That(content.Hash, Contains.Substring("2018-01-24-Chopok0335.JPGHash"));
        }

        [Test]
        public void JpgElement_IsImageType()
        {
            var physicalFilesParser = new FileImportTestData().CreateTestPhysicalFilesParser();

            var content = physicalFilesParser.ParsePhysicalFiles(CreateThreadInfo()).First();

            Assert.That(content.Type, Is.EqualTo(ContentTypeEnum.Image));
        }

        [Test]
        public void ParsingFiles_ChangesFilesAndFilesDoneCounts()
        {
            var physicalFilesParser = new FileImportTestData().CreateTestPhysicalFilesParser();

            var info = CreateThreadInfo();
            physicalFilesParser.ParsePhysicalFiles(info).First();

            Assert.That(info.Files, Is.EqualTo(5));
            Assert.That(info.FilesDone, Is.EqualTo(1));

            physicalFilesParser.ParsePhysicalFiles(info).Last();
            Assert.That(info.Files, Is.EqualTo(5));
            Assert.That(info.FilesDone, Is.EqualTo(5));
        }

        [Test]
        public void Convert_NewFile_ReturnsIt()
        {
            var physicalFilesParser = new FileImportTestData().CreateTestPhysicalFilesParser();

            var it = physicalFilesParser.ParsePhysicalFiles(CreateThreadInfo()).First();

            Assert.That(it, Is.Not.Null);
            Assert.That(it.Hash, Is.EqualTo("asdf"));
            Assert.That(it.Label, Is.EqualTo("asdf.jpg"));
            Assert.That(it.Type, Is.EqualTo("Image"));

            Assert.That(it.Binders.Count(), Is.EqualTo(1));
            Assert.That(it.AttributedBinders.Count(), Is.EqualTo(1));
        }

        private DirectoryContentThreadInfo CreateThreadInfo()
        {
            return new DirectoryContentThreadInfo { FullPath = CoreTestData.CurentDirectory };
        }
    }
}