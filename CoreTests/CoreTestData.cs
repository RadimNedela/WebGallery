using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.Tests
{
    public class CoreTestData
    {
        [Test]
        public void CoreTestData_AreConstructedWell()
        {
            var dbInfo = new CoreTestData().TestDatabase;

            Assert.That(dbInfo.Racks.Count, Is.GreaterThan(1), "Test database should contain at least 2 racks, add them...");
            foreach (var rack in dbInfo.Racks)
            {
                Assert.That(rack.MountPoints.Any(), $"Rack {rack} does not contain any mount points");
            }
        }

        private DatabaseInfo _testDatabase;
        public DatabaseInfo TestDatabase
        {
            get
            {
                return _testDatabase ??= CreateTestDatabase();
            }
        }

        private DatabaseInfo CreateTestDatabase()
        {
            var builder = DatabaseInfoTestDataBuilder.CreateDefault()
                .Add(RackTestDataBuilder.CreateDefault()
                    .Add(MountPointTestDataBuilder.CreateWindowsDefault())
                    .Add(MountPointTestDataBuilder.CreateLinuxDefault())
                    )
                .Add(RackTestDataBuilder.CreateDefault().WithName("Second Test Rack")
                    .Add(MountPointTestDataBuilder.CreateWindowsDefault().WithPath(@"D:\TEMP"))
                    );
            return builder.Build();
        }

        public IDatabaseInfoRepository CreateTestDatabaseRepositorySubstitute()
        {
            IDatabaseInfoRepository repository = Substitute.For<IDatabaseInfoRepository>();
            var testDatabase = TestDatabase;
            repository.GetAll().Returns(new List<DatabaseInfo> { testDatabase });
            repository.Get(testDatabase.Hash).Returns(testDatabase);

            return repository;
        }

        public IGalerySession CreateTestDatabaseSession()
        {
            IGalerySession session = Substitute.For<IGalerySession>();
            session.ActiveDatabaseHash.Returns(TestDatabase.Hash);
            session.ActiveRackHash.Returns(TestDatabase.Racks.First().Hash);
            return session;
        }

        public const string CurentDirectory = @"C:\Temp";

        public IDirectoryMethods CreateTestDirectoryMethods()
        {
            var directoryMethods = Substitute.For<IDirectoryMethods>();
            directoryMethods.GetCurrentDirectoryName().Returns(CurentDirectory);
            directoryMethods.GetFileNames(Arg.Any<string>()).Returns(new[] {
                        CurentDirectory + "\\2018-01-24-Chopok0335.JPG",
                        CurentDirectory + "\\2018-01-24-Chopok0357.JPG",
                        CurentDirectory + "\\2018-01-24-Chopok0361.JPG",
                        CurentDirectory + "\\2018-01-24-Chopok0366.JPG",
                        CurentDirectory + "\\2018-01-24-Chopok0366ASDF.JPG"
                    });
            directoryMethods.GetDirectories(Arg.Any<string>()).Returns(
                p => new List<string> {
                    p.ArgAt<string>(0) + @"\TestSubDir1",
                    p.ArgAt<string>(0) + @"\TestSubDir2" }
                );
            return directoryMethods;
        }

        public IHasher CreateTestHasher()
        {
            var hasher = Substitute.For<IHasher>();
            hasher.ComputeFileContentHash(Arg.Any<string>()).Returns(ci => $"Hash{ci.ArgAt<string>(0)}Hash".Replace("ASDF", ""));
            return hasher;
        }

        public IActiveDatabaseInfoProvider CreateTestCurrentDatabaseInfoProvider()
        {
            var rack = Substitute.For<IActiveRackService>();
            rack.Name.Returns("Test database rack name");
            rack.ActiveDirectory.Returns(CurentDirectory);
            rack.GetSubpath(Arg.Any<string>()).Returns(s => s.ArgAt<string>(0).Substring(CurentDirectory.Length + 1));

            var databaseInfo = Substitute.For<IDatabaseInfo>();
            databaseInfo.CurrentDatabaseName.Returns("Test database info name");
            databaseInfo.ActiveRack.Returns(rack);

            var infoProvider = Substitute.For<IActiveDatabaseInfoProvider>();
            infoProvider.CurrentInfo.Returns(databaseInfo);

            return infoProvider;
        }
    }
}
