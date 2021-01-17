using NSubstitute;
using NUnit.Framework;
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
                Type = ContentTypeEnum.ImageType
            });

            Assert.That(it, Is.Not.Null);
        }

        private PhysicalFileToEntity CreateSUT()
        {
            IContentEntityRepository repository = Substitute.For<IContentEntityRepository>();

            PhysicalFileToEntity sut = new(repository);
            return sut;
        }
    }
}
