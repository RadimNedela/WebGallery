using NUnit.Framework;
using System.Linq;
using WebGalery.Domain.Contents.Factories;
using WebGalery.Domain.FileServices;

namespace DomainTests.Integration
{
    [TestFixture]
    public class HasherTests
    {
        [Test]
        public void LoadDirectory_Duha383_IsThereTwiceWithSameHash()
        {
            BinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            var duhy = binder.ChildBinders.First(b => b.Name == "Duha");
            var duha383_1 = duhy.Displayables.First(d => d.Name == "2017-08-20-Duha0383.JPG");
            var duha383_2 = duhy.Displayables.First(d => d.Name == "2017-08-20-Duha0383_2.JPG");
            Assert.That(duha383_1.Hash, Is.EqualTo(duha383_2.Hash));
        }

        private static BinderFactory BuildSut()
        {
            var directoryReader = new DirectoryMethods();
            var fileReader = new FileMethods();
            var hasher = new Sha1Hasher();
            var displayableFactory = new DisplayableFactory(fileReader, hasher);
            var binderFactory = new BinderFactory(directoryReader, displayableFactory);
            return binderFactory;
        }
    }
}
