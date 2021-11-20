using System.Linq;
using NSubstitute;
using NUnit.Framework;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.FileImport;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Maintenance;

namespace WebGalery.Core.Tests.FileImport
{
    /// <summary>
    /// Nádhera:
    ///     What is recommended way to create test data for unit test cases?
    ///     
    /// Use a whiteboard
    /// 
    ///     To compute this schedule, I need to interact with 25-30 entities and each entity, on average, have 10 fields.
    ///     
    /// No.You need to go back to the whiteboard.
    /// </summary>
    [TestFixture]
    public class PhysicalFilesParserTests
    {
        [Test]
        public void ParsePhysicalFiles_ValidPath_ReturnsCorrectNumberOfPictures()
        {
            var fixture = new TestFixture();
            var physicalFilesParser = fixture.BuildParser();

            var files = physicalFilesParser.ParsePhysicalFiles(fixture.BuildThreadInfo());

            Assert.That(files.Count(), Is.EqualTo(5));
        }

        [Test]
        public void ParsePhysicalFiles_ValidPath_FirstElementHasCorrectHash()
        {
            var fixture = new TestFixture();
            var physicalFilesParser = fixture.BuildParser();

            var content = physicalFilesParser.ParsePhysicalFiles(fixture.BuildThreadInfo()).First();

            Assert.That(content.Hash, Contains.Substring("2018-01-24-Chopok0335.JPGHash"));
        }

        [Test]
        public void JpgElement_IsImageType()
        {
            var fixture = new TestFixture();
            var physicalFilesParser = fixture.BuildParser();

            var content = physicalFilesParser.ParsePhysicalFiles(fixture.BuildThreadInfo()).First();

            Assert.That(content.Type, Is.EqualTo(ContentTypeEnum.Image));
        }

        [Test]
        public void ParsingFiles_ChangesFilesAndFilesDoneCounts()
        {
            var fixture = new TestFixture();
            var physicalFilesParser = fixture.BuildParser();

            var info = fixture.BuildThreadInfo();
            physicalFilesParser.ParsePhysicalFiles(info).First();

            Assert.That(info.ThreadInfoDto.Files, Is.EqualTo(5));
            Assert.That(info.ThreadInfoDto.FilesDone, Is.EqualTo(1));

            physicalFilesParser.ParsePhysicalFiles(info).Last();
            Assert.That(info.ThreadInfoDto.Files, Is.EqualTo(5));
            Assert.That(info.ThreadInfoDto.FilesDone, Is.EqualTo(5));
        }

        [Test]
        public void ParsingFile_NewFile_ReturnsValidContentEntity()
        {
            var fixture = new TestFixture();
            var physicalFilesParser = fixture.BuildParser();

            var it = physicalFilesParser.ParsePhysicalFiles(fixture.BuildThreadInfo()).First();

            Assert.That(it, Is.Not.Null);
            Assert.That(it.Hash, Does.StartWith("Hash").And.EndsWith("Hash"));
            //Assert.That(it.Label, Is.EqualTo("2018-01-24-Chopok0335.JPG"));
            Assert.That(it.Type, Is.EqualTo("Image"));

            //Assert.That(it.AttributedBinders, Is.Not.Null, "Attributed Binders is null");
            //Assert.That(it.AttributedBinders.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ParsingFile_ReturnsValidDirectoryBinder()
        {
            var fixture = new TestFixture();
            var physicalFilesParser = fixture.BuildParser();

            //var ab = physicalFilesParser.ParsePhysicalFiles(fixture.BuildThreadInfo()).First().AttributedBinders.First();

            //Assert.That(ab.Attribute, Does.Contain("Chopok0335"));
        }

        private class TestFixture
        {
            private readonly IHasher hasher;
            private readonly IActiveRackService activeRackService;

            public TestFixture()
            {
                hasher = Substitute.For<IHasher>();
                activeRackService = Substitute.For<IActiveRackService>();
            }

            public PhysicalFilesParser BuildParser()
            {
                return new PhysicalFilesParser(hasher, activeRackService);
            }

            public DirectoryContentThreadInfo BuildThreadInfo()
            {
                return new DirectoryContentThreadInfo() { 
                    FullPath = "My Test Path",
                    ThreadInfoDto = new DirectoryContentThreadInfoDto(),
                    FileNames = new string[]
                    {
                        "1.jpg",
                        "2.jpg"
                    }
                };
            }
        }
    }
}