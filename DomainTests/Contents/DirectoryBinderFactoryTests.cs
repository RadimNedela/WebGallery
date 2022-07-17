using DomainTests;
using Moq;
using NUnit.Framework;
using System.Linq;
using WebGalery.Domain.Basics;
using WebGalery.Domain.Warehouses.Loaders;

namespace WebGalery.Domain.Tests.Contents
{
    [TestFixture]
    public class DirectoryBinderFactoryTests
    {
        [Test]
        public void LoadDirectory_TestDirectory_Creates2ChildBinders()
        {
            FileSystemDirectoryLoader binderFactory = new TestFixture().BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.ChildBinders.Count, Is.EqualTo(2));
        }

        [Test]
        public void LoadDirectory_TestDirectory_Creates9Displayables()
        {
            FileSystemDirectoryLoader binderFactory = new TestFixture().BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.NumberOfDisplayables, Is.EqualTo(9));
        }

        [Test]
        public void LoadDirectory_Twice_DoesNotDoubleDisplayables()
        {
            FileSystemDirectoryLoader binderFactory = new TestFixture().BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");
            var binder2 = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder, Is.SameAs(binder2));
            Assert.That(binder.NumberOfDisplayables, Is.EqualTo(9));
        }

        [Test]
        public void LoadDirectory_TwiceWithDifferentHashes_RemovesOriginals()
        {
            var fixture = new TestFixture().GenerateRandomHashes();
            FileSystemDirectoryLoader binderFactory = fixture.BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");
            var file1 = binder.ChildBinders.First().Displayables.First();
            var binder2 = binderFactory.LoadDirectory("TestPictures");
            var file2 = binder2.ChildBinders.First().Displayables.First();

            Assert.That(file1.Name, Is.EqualTo(file2.Name));
            Assert.That(file1.Hash, Is.Not.EqualTo(file2.Hash));
        }

        [Test]
        public void LoadDirectory_TestDirectory_Has0DirectImages()
        {
            FileSystemDirectoryLoader binderFactory = new TestFixture().BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.Displayables.Count, Is.EqualTo(0));
        }

        [Test]
        public void LoadDirectory_RootBinderName_IsTestPictures()
        {
            FileSystemDirectoryLoader binderFactory = new TestFixture().BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.Name, Is.EqualTo("TestPictures"));
        }

        private class TestFixture
        {
            public Mock<IHasher> HasherMock { get; private set; }
            private int _runningNumber;

            public TestFixture GenerateRandomHashes()
            {
                HasherMock = new Mock<IHasher>();
                HasherMock.Setup(h => h.ComputeFileContentHash(It.IsAny<string>())).Returns(() => _runningNumber++.ToString());
                return this;
            }

            public FileSystemDirectoryLoader BuildSut()
            {
                var mother = new ObjectMother();
                if (HasherMock == null)
                {
                    HasherMock = new Mock<IHasher>();
                    HasherMock.Setup(h => h.ComputeFileContentHash(It.IsAny<string>())).Returns<string>(x => x);
                }
                mother.Hasher = HasherMock.Object;

                return mother.DirectoryBinderFactory;
            }
        }
    }
}
