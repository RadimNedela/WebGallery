using DomainTests;
using Moq;
using NUnit.Framework;
using System.Linq;
using WebGalery.Domain.Basics;
using WebGalery.Domain.Warehouses.Loaders;

namespace WebGalery.Domain.Tests.Warehouses.Loaders
{
    [TestFixture]
    public class FileSystemDirectoryLoaderTests
    {
        [Test]
        public void LoadDirectory_TestDirectory_Creates2ChildRacks()
        {
            FileSystemDirectoryLoader sut = new TestFixture().BuildSut();

            var toTest = sut.LoadDirectory("TestPictures");

            Assert.That(toTest.Racks.Count, Is.EqualTo(2));
        }

        [Test]
        public void LoadDirectory_TestDirectory_Creates9Displayables()
        {
            FileSystemDirectoryLoader sut = new TestFixture().BuildSut();

            var toTest = sut.LoadDirectory("TestPictures");

            Assert.That(toTest.NumberOfStorables, Is.EqualTo(9));
        }

        [Test]
        public void LoadDirectory_Twice_DoesNotDoubleDisplayables()
        {
            FileSystemDirectoryLoader sut = new TestFixture().BuildSut();

            var toTest1 = sut.LoadDirectory("TestPictures");
            var toTest2 = sut.LoadDirectory("TestPictures");

            Assert.That(toTest1, Is.SameAs(toTest2));
            Assert.That(toTest1.NumberOfStorables, Is.EqualTo(9));
        }

        [Test]
        public void LoadDirectory_TwiceWithDifferentHashes_RemovesOriginals()
        {
            var fixture = new TestFixture().GenerateRandomHashes();
            FileSystemDirectoryLoader sut = fixture.BuildSut();

            var toTest1 = sut.LoadDirectory("TestPictures");
            var file1 = toTest1.Racks.First().Storables.First();
            var toTest2 = sut.LoadDirectory("TestPictures");
            var file2 = toTest2.Racks.First().Storables.First();

            Assert.That(file1.Name, Is.EqualTo(file2.Name));
            Assert.That(file1.EntityHash, Is.Not.EqualTo(file2.EntityHash));
        }

        [Test]
        public void LoadDirectory_TestDirectory_Has0DirectImages()
        {
            FileSystemDirectoryLoader sut = new TestFixture().BuildSut();

            var toTest = sut.LoadDirectory("TestPictures");

            Assert.That(toTest.Storables.Count, Is.EqualTo(0));
        }

        [Test]
        public void LoadDirectory_RootBinderName_IsTestPictures()
        {
            FileSystemDirectoryLoader sut = new TestFixture().BuildSut();

            var toTest = sut.LoadDirectory("TestPictures");

            Assert.That(toTest.Name, Is.EqualTo("TestPictures"));
        }

        private class TestFixture
        {
            private int _runningNumber;
            private bool _generateRandomHashes = false;

            public TestFixture GenerateRandomHashes()
            {
                _generateRandomHashes = true;
                return this;
            }

            public FileSystemDirectoryLoader BuildSut()
            {
                var mother = new ObjectMother();
                var hasherMock = new Mock<IHasher>();
                if (_generateRandomHashes)
                    hasherMock.Setup(h => h.ComputeFileContentHash(It.IsAny<string>())).Returns(() => _runningNumber++.ToString());
                else
                    hasherMock.Setup(h => h.ComputeFileContentHash(It.IsAny<string>())).Returns<string>(x => x);
                hasherMock.Setup(h => h.ComputeRandomStringHash(It.IsAny<string>())).Returns<string>(x => x);
                hasherMock.Setup(h => h.ComputeDependentStringHash(It.IsAny<IEntity>(), It.IsAny<string>())).Returns<IEntity, string>((e, x) => x);
                mother.Hasher = hasherMock.Object;

                return mother.FileSystemDirectoryLoader;
            }
        }
    }
}
