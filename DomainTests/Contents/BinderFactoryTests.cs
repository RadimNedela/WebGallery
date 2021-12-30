using NUnit.Framework;
using System.Linq;
using WebGalery.Domain.Contents.Factories;
using WebGalery.Domain.FileServices;

namespace DomainTests.Contents
{
    internal class BinderFactoryTests
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

        [Test]
        public void LoadDirectory_Duha383_IsThereTwiceWithSameHash()
        {
            BinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            var duhy = binder.ChildBinders.First(b => b.Name == "Duha");
            Assert.That(duhy, Is.Not.Empty);
            var duha383_1 = duhy.Displayables.First(d => d.Name == "2017-08-20-Duha0383.JPG");
            var duha383_2 = duhy.Displayables.First(d => d.Name == "2017-08-20-Duha0383_2.JPG");
        }

        private static BinderFactory BuildSut()
        {
            var directoryReader = new DirectoryMethods();
            var fileReader = new FileMethods();
            var displayableFactory = new DisplayableFactory(fileReader);
            var binderFactory = new BinderFactory(directoryReader, displayableFactory);
            return binderFactory;
        }
    }
}
