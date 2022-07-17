using NUnit.Framework;
using System.IO;
using System.Linq;
using WebGalery.Domain.FileServices;

namespace WebGalery.Domain.Tests.FileServices
{
    [TestFixture]
    public class FileSystemLocationTests
    {
        [Test]
        public void NormalizePath_ValidWindowsPath4Directories_Returns4Elements()
        {
            var sut = new FileSystemLocation(new DirectoryMethods());

            var toTest = sut.SplitJourneyToLegs(@"a\b\c\d");

            Assert.That(toTest.Count(), Is.EqualTo(4));
        }

        [Test]
        public void NormalizePath_ValidWindowsPath4DirectoriesEndingWithBackslash_Returns4Elements()
        {
            var sut = new FileSystemLocation(new DirectoryMethods());

            var toTest = sut.SplitJourneyToLegs(@"a\b\c\d\");

            Assert.That(toTest.Count(), Is.EqualTo(4));
        }

        [Test]
        public void NormalizePath_ValidWindowsPathIncludingRoot_Returns4Elements()
        {
            var sut = new FileSystemLocation(new DirectoryMethods());

            var toTest = sut.SplitJourneyToLegs(Directory.GetCurrentDirectory() + @"\a\b\c\d");

            Assert.That(toTest.Count(), Is.EqualTo(4));
        }

        [Test]
        public void NormalizePath_ValidPath_ReturnsCorrectValues()
        {
            var sut = new FileSystemLocation(new DirectoryMethods());

            var toTest = sut.SplitJourneyToLegs(@"a\b").ToList();

            Assert.That(toTest[0], Is.EqualTo("a"));
            Assert.That(toTest[1], Is.EqualTo("b"));
        }
    }
}
