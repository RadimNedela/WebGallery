using NUnit.Framework;
using System.IO;
using System.Linq;
using WebGalery.Domain.FileServices;

namespace DomainTests.FileServices
{
    [TestFixture]
    public class FileSystemRootPathTests
    {
        [Test]
        public void NormalizePath_ValidWindowsPath4Directories_Returns4Elements()
        {
            var sut = new FileSystemRootPath(new DirectoryMethods());

            var toTest = sut.SplitPath(@"a\b\c\d");

            Assert.That(toTest.Count(), Is.EqualTo(4));
        }

        [Test]
        public void NormalizePath_ValidWindowsPath4DirectoriesEndingWithBackslash_Returns4Elements()
        {
            var sut = new FileSystemRootPath(new DirectoryMethods());

            var toTest = sut.SplitPath(@"a\b\c\d\");

            Assert.That(toTest.Count(), Is.EqualTo(4));
        }

        [Test]
        public void NormalizePath_ValidWindowsPathIncludingRoot_Returns4Elements()
        {
            var sut = new FileSystemRootPath(new DirectoryMethods());

            var toTest = sut.SplitPath(Directory.GetCurrentDirectory() + @"\a\b\c\d");

            Assert.That(toTest.Count(), Is.EqualTo(4));
        }

        [Test]
        public void NormalizePath_ValidPath_ReturnsCorrectValues()
        {
            var sut = new FileSystemRootPath(new DirectoryMethods());

            var toTest = sut.SplitPath(@"a\b").ToList();

            Assert.That(toTest[0], Is.EqualTo("a"));
            Assert.That(toTest[1], Is.EqualTo("b"));
        }
    }
}
