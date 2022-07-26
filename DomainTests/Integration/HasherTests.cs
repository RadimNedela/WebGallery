using NUnit.Framework;
using System.IO;
using System.Linq;

namespace DomainTests.Integration
{
    [TestFixture]
    public class HasherTests
    {
        [Test]
        public void LoadDirectory_HasherIntegrationTests()
        {
            var mother = new ObjectMother();
            var sut = mother.FileSystemDirectoryLoader;

            var toTest = sut.LoadDirectory("TestPictures");

            // 2 same pictures with different metadata must generate same hash
            var duhy = toTest.Racks.First(b => b.Name == "Duha");
            var duha383_1 = duhy.Storables.First(d => d.Name == "2017-08-20-Duha0383.JPG");
            var duha383_2 = duhy.Storables.First(d => d.Name == "2017-08-20-Duha0383_2.JPG");
            Assert.That(duha383_1.EntityHash, Is.EqualTo(duha383_2.EntityHash), "same picture different only in metadata must have same hash");

            // binders must have different hashes
            var chopky = toTest.Racks.First(b => b.Name == "Chopok");
            Assert.That(toTest.Hash, Is.Not.EqualTo(duhy.Hash), "different directories must have different hash, root == duhy");
            Assert.That(toTest.Hash, Is.Not.EqualTo(chopky.Hash), "different directories must have different hash, root == chopky");
            Assert.That(chopky.Hash, Is.Not.EqualTo(duhy.Hash), "different directories must have different hash, duhy == chopky");

            // same directory got differently must have the same hash
            var chopky2 = sut.LoadDirectory(Directory.GetCurrentDirectory() + "/TestPictures/Chopok");
            Assert.That(chopky.Hash, Is.EqualTo(chopky2.Hash), "same directory loaded differently must have same hash");
        }
    }
}
