using NUnit.Framework;
using System.Linq;
using WebGalery.Domain.FileServices;

namespace DomainTests.FileServices
{
    [TestFixture]
    public class FileSystemRootPathTests
    {
        [Test]
        public void NormalizePath_ValidWindowsPath4Directories_Returns5Elements()
        {
            var sut = new FileSystemRootPath(new DirectoryMethods());

            var toTest = sut.NormalizePath(@"a\b\c\d");

            Assert.That(toTest.Count(), Is.EqualTo(5));
        }
    }
}
