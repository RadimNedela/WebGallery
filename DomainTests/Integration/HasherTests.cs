using NUnit.Framework;
using System.IO;
using System.Linq;
using WebGalery.Domain.Contents.Factories;
using WebGalery.Domain.FileServices;

namespace DomainTests.Integration
{
    [TestFixture]
    public class HasherTests
    {
        [Test]
        public void LoadDirectory_HasherIntegrationTests()
        {
            DirectoryBinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            // 2 same pictures with different metadata must generate same hash
            var duhy = binder.ChildBinders.First(b => b.Name == "Duha");
            var duha383_1 = duhy.Displayables.First(d => d.Name == "2017-08-20-Duha0383.JPG");
            var duha383_2 = duhy.Displayables.First(d => d.Name == "2017-08-20-Duha0383_2.JPG");
            Assert.That(duha383_1.Hash, Is.EqualTo(duha383_2.Hash), "same picture different only in metadata must have same hash");

            // binders must have different hashes
            var chopky = binder.ChildBinders.First(b => b.Name == "Chopok");
            Assert.That(binder.Hash, Is.Not.EqualTo(duhy.Hash), "different directories must have different hash, root == duhy");
            Assert.That(binder.Hash, Is.Not.EqualTo(chopky.Hash), "different directories must have different hash, root == chopky");
            Assert.That(chopky.Hash, Is.Not.EqualTo(duhy.Hash), "different directories must have different hash, duhy == chopky");

            // same directory got differently must have the same hash
            var chopky2 = binderFactory.LoadDirectory(Directory.GetCurrentDirectory() + "/TestPictures/Chopok");
            Assert.That(chopky.Hash, Is.EqualTo(chopky2.Hash), "same directory loaded differently must have same hash");
        }

        private static DirectoryBinderFactory BuildSut()
        {
            var directoryReader = new DirectoryMethods();
            var fileReader = new FileMethods();
            var hasher = new Sha1Hasher();
            var displayableFactory = new DisplayableFactory(fileReader, hasher);
            var binderFactory = new DirectoryBinderFactory(directoryReader, displayableFactory, hasher);
            return binderFactory;
        }
    }
}
