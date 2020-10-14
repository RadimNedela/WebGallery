using Domain.InfrastructureInterfaces;
using Domain.Logging;
using Domain.Services;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests.DbEntities
{
    [TestFixture]
    public class DatabaseInfoBuilderTests
    {
        private static readonly ISimpleLogger log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Test]
        public void ExampleDatabaseInfo_CanBeCreated()
        {
            var hasher = Substitute.For<IHasher>();
            hasher.ComputeStringHash(Arg.Any<string>()).Returns(info => "HASH" + info.ArgAt<string>(0) + "HASH");

            var databaseBuilder = new DatabaseInfoBuilder(hasher);
            var entity = databaseBuilder.BuildNewDatabase("asdf");

            log.Debug($"povedlo se mi vytvořit {entity.Hash}");
            Assert.That(entity.Name, Is.EqualTo("asdf"));
        }
    }
}
