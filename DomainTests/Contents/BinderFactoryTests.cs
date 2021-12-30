using Moq;
using NUnit.Framework;
using System.Linq;
using WebGalery.Domain.Contents.Factories;
using WebGalery.Domain.FileServices;

namespace DomainTests.Contents
{
    [TestFixture]
    public class BinderFactoryTests
    {
        [Test]
        public void LoadDirectory_TestDirectory_Creates2ChildBinders()
        {
            BinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.ChildBinders.Count, Is.EqualTo(2));
        }

        [Test]
        public void LoadDirectory_TestDirectory_Creates9Displayables()
        {
            BinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.NumberOfDisplayables, Is.EqualTo(9));
        }

        [Test]
        public void LoadDirectory_TestDirectory_Has0DirectImages()
        {
            BinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.Displayables.Count, Is.EqualTo(0));
        }

        [Test]
        public void LoadDirectory_RootBinderName_IsTestPictures()
        {
            BinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.Name, Is.EqualTo("TestPictures"));
        }

        private static BinderFactory BuildSut()
        {
            var directoryReader = new DirectoryMethods();
            var fileReader = new FileMethods();
            var hasherMock = new Mock<IHasher>();
            hasherMock.Setup(h => h.ComputeFileContentHash(It.IsAny<string>())).Returns<string>(x => x);
            var displayableFactory = new DisplayableFactory(fileReader, hasherMock.Object);
            var binderFactory = new BinderFactory(directoryReader, displayableFactory);
            return binderFactory;
        }
    }
}
