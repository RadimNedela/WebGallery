using NSubstitute;
using NUnit.Framework;
using System.Linq;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.FileImport.Domain;

namespace FileImportTests.Domain
{
    [TestFixture]
    public class PhysicalFileToEntityTests
    {
        [Test]
        public void Convert_NewFile_ReturnsIt()
        {
            var sut = CreateSUT();

            var it = sut.ToContentEntity(new PhysicalFile()
            {
                FullPath = @"/tmp/asdf.jpg",
                Hash = "asdf",
                Type = ContentTypeEnum.Image
            });

            Assert.That(it, Is.Not.Null);
            Assert.That(it.Hash, Is.EqualTo("asdf"));
            Assert.That(it.Label, Is.EqualTo("asdf.jpg"));
            Assert.That(it.Type, Is.EqualTo("Image"));

            Assert.That(it.Binders.Count(), Is.EqualTo(1));
            Assert.That(it.AttributedBinders.Count(), Is.EqualTo(1));
        }

        private PhysicalFileToEntity CreateSUT()
        {
            IContentEntityRepository repository = Substitute.For<IContentEntityRepository>();
            Binder binder = new Binder(null, null);

            PhysicalFileToEntity sut = new(repository, binder);
            return sut;
        }
    }
}
