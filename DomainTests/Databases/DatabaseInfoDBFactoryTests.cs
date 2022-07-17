using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebGalery.Domain.Databases;
using WebGalery.Domain.DBModel.Factories;
using WebGalery.Domain.FileServices;
using WebGalery.Domain.Warehouses;

namespace ApplicationTests.Databases
{
    [TestFixture]
    public class DatabaseInfoDBFactoryTests
    {
        [Test]
        public void Build_ValidDomain_FillsTheValues()
        {
            var factory = new DatabaseInfoDBFactory(new Sha1Hasher());
            var domainEntity = new Depository() { Hash = "Test Hash", Name = "Test Name" };

            var database = factory.Build(domainEntity);

            Assert.That(database.Hash, Is.EqualTo("Test Hash"), "Hash differs");
            Assert.That(database.Name, Is.EqualTo("Test Name"), "Name differs");
        }

        [Test]
        public void Build_DomainWithRacks_NumberOfDifferentRacksSits()
        {
            var factory = new DatabaseInfoDBFactory(new Sha1Hasher());
            var domainEntity = new Depository()
            {
                Racks = new List<Depot>
                {
                    new Depot { Hash = "1234"},
                    new Depot { Hash = "1234"},
                    new Depot { Hash = "5678"},
                }
            };

            var database = factory.Build(domainEntity);

            Assert.That(database.Racks.Count, Is.EqualTo(2));
        }

        [Test]
        public void Build_DomainWithRack_FillsRackValues()
        {
            var factory = new DatabaseInfoDBFactory(new Sha1Hasher());
            var domainEntity = new Depository()
            {
                Hash = "Database Test Hash",
                Racks = new List<Depot>
                {
                    new Depot { Hash = "1234",
                    Name = "Rack Test Name",
                    },
                }
            };

            var rack = factory.Build(domainEntity).Racks.First();
            Assert.That(rack.Hash, Is.EqualTo("1234"), "Hash differs");
            Assert.That(rack.Name, Is.EqualTo("Rack Test Name"), "Name differs");
            Assert.That(rack.DatabaseHash, Is.EqualTo("Database Test Hash"), "DatabaseHash differs");
            Assert.That(rack.Database, Is.Not.Null, "Database is not set");
        }

        [Test]
        public void Build_DomainWithRackAndMountPoints_NumberOfMountPointsSits()
        {
            var factory = new DatabaseInfoDBFactory(new Sha1Hasher());
            IDirectoryReader directoryReader = Substitute.For<IDirectoryReader>();
            var rootPaths = new List<ILocation>();
            directoryReader.GetCurrentDirectoryName().Returns("DirectoryName1");
            rootPaths.Add(new FileSystemRootPath(directoryReader));
            rootPaths.Add(new FileSystemRootPath(directoryReader));
            directoryReader.GetCurrentDirectoryName().Returns("DirectoryName2");
            rootPaths.Add(new FileSystemRootPath(directoryReader));
            var domainEntity = new Depository()
            {
                Racks = new List<Depot>
                {
                    new Depot
                    {
                        Hash = "1234",
                        RootPaths = rootPaths
                    }
                }
            };

            var rack = factory.Build(domainEntity).Racks.First();

            Assert.That(rack.MountPoints.Count, Is.EqualTo(2));
        }

        [Test]
        public void Build_DomainWithRackAndMountPoint_MountPointSetsTheValues()
        {
            var factory = new DatabaseInfoDBFactory(new Sha1Hasher());
            var domainEntity = new Depository()
            {
                Racks = new List<Depot>
                {
                    new Depot
                    {
                        Hash = "1234",
                        RootPaths = new List<ILocation>{ new FileSystemRootPath(new DirectoryMethods()) }
                    }
                }
            };

            var mp = factory.Build(domainEntity).Racks.First().MountPoints.First();

            Assert.That(mp.Hash.Length, Is.GreaterThan(10), "Hash is not set");
            Assert.That(mp.Path.Length, Is.GreaterThan(5), "Path is not set");
            Assert.That(mp.RackHash, Is.EqualTo("1234"), "Rack hash is not set");
            Assert.That(mp.Rack, Is.Not.Null, "Rack is not set");
        }
    }
}
