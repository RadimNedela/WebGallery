using Moq;
using NUnit.Framework;
using WebGalery.Domain;
using WebGalery.Domain.Contents.Factories;
using WebGalery.Domain.FileServices;

namespace DomainTests.Contents
{
    [TestFixture]
    public class DirectoryBinderFactoryTests
    {
        [Test]
        public void LoadDirectory_TestDirectory_Creates2ChildBinders()
        {
            DirectoryBinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.ChildBinders.Count, Is.EqualTo(2));
        }

        [Test]
        public void LoadDirectory_TestDirectory_Creates9Displayables()
        {
            DirectoryBinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.NumberOfDisplayables, Is.EqualTo(9));
        }

        [Test]
        public void LoadDirectory_TestDirectory_Has0DirectImages()
        {
            DirectoryBinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.Displayables.Count, Is.EqualTo(0));
        }

        [Test]
        public void LoadDirectory_RootBinderName_IsTestPictures()
        {
            DirectoryBinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.Name, Is.EqualTo("TestPictures"));
        }

        private static DirectoryBinderFactory BuildSut()
        {
            var mother = new ObjectMother();
            var hasherMock = new Mock<IHasher>();
            hasherMock.Setup(h => h.ComputeFileContentHash(It.IsAny<string>())).Returns<string>(x => x);
            var displayableFactory = new DisplayableFactory(new FileMethods(), hasherMock.Object);
            var binderFactory = new DirectoryBinderFactory(
                new DirectoryMethods(), displayableFactory, hasherMock.Object, new SessionProvider(mother.Session));
            return binderFactory;
        }
    }
}
