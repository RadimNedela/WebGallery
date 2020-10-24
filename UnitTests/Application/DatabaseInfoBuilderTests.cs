using Domain.Elements.Maintenance;
using Domain.InfrastructureInterfaces;
using Domain.Logging;
using Domain.Services;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.DbEntities
{
    [TestFixture]
    public class DatabaseInfoBuilderTests
    {
        private static readonly ISimpleLogger log = new MyOwnLog4NetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Create new database info
        private DatabaseInfoBuilder GetTestBuilder()
        {
            var hasher = Substitute.For<IHasher>();
            hasher.ComputeStringHash(Arg.Any<string>()).Returns(info => "HASH" + info.ArgAt<string>(0) + "HASH");
            var directoryMethods = Substitute.For<IDirectoryMethods>();
            directoryMethods.GetCurrentDirectoryName().Returns("f:\nonsenseDirectory");

            var databaseBuilder = new DatabaseInfoBuilder(hasher, null, directoryMethods);
            return databaseBuilder;
        }

        [Test]
        public void ExampleDatabaseInfo_CanBeCreated()
        {
            var databaseBuilder = GetTestBuilder();
            var element = databaseBuilder.BuildNewDatabase("asdf");

            log.Debug($"povedlo se mi vytvořit {element.Hash}");
            Assert.That(element.Name, Is.EqualTo("asdf"));
        }

        [Test]
        public void NewDatabaseInfo_EntityName_IsCorrect()
        {
            var databaseBuilder = GetTestBuilder();
            var element = databaseBuilder.BuildNewDatabase("asdf");

            Assert.That(element.Entity.Name, Is.EqualTo("asdf"));
        }

        [Test]
        public void NewDatabaseInfo_EntityHash_IsCorrect()
        {
            var databaseBuilder = GetTestBuilder();
            var element = databaseBuilder.BuildNewDatabase("asdf");

            Assert.That(element.Entity.Hash, Does.Contain("HASHasdf"));
        }
        #endregion Create new database info

        private DatabaseInfoElement GetTestElement()
        {
            var builder = GetTestBuilder();
            var testElement = builder.BuildNewDatabase("TestDatabase");
            return testElement;
        }

        [Test]
        public void AddNewRack_AddsItToTheEntity()
        {
            var element = GetTestElement();

            element.AddNewRack("MetArt", @"t:\MetArt");

            Assert.That(element.Entity.Racks.Last().Name, Is.EqualTo("MetArt"));
        }

        [Test]
        public void AddNewRack_InitialMountPoint_WillBeAddedToEntity()
        {
            var element = GetTestElement();

            element.AddNewRack("MetArt", @"\mount\externalDisk01\MetArt");

            Assert.That(element.Entity.Racks.Last().MountPoints.First().Path, Does.Contain("externalDisk01"));
        }
    }
}
