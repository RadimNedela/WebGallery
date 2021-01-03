using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using WebGalery.Core.DbEntities.Maintenance;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.Tests
{
    public class MaintenanceTestData
    {
        private DatabaseInfoEntity testDatabase;
        public DatabaseInfoEntity TestDatabase
        {
            get
            {
                return testDatabase ??= CreateTestDatabase();
            }
        }

        private DatabaseInfoEntity CreateTestDatabase()
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

        public IDatabaseInfoEntityRepository CreateTestDatabaseRepositorySubstitute()
        {
            IDatabaseInfoEntityRepository repository = Substitute.For<IDatabaseInfoEntityRepository>();
            var testDatabase = TestDatabase;
            repository.GetAll().Returns(new List<DatabaseInfoEntity> { testDatabase });
            repository.Get(testDatabase.Hash).Returns(testDatabase);

            return repository;
        }

        public IGalerySession CreateTestDatabaseSession()
        {
            IGalerySession session = Substitute.For<IGalerySession>();
            session.CurrentDatabaseHash.Returns(TestDatabase.Hash);
            session.CurrentRackHash.Returns(TestDatabase.Racks.First().Hash);
            return session;
        }
    }
}
