using CoreTests;
using Domain.DbEntities.Maintenance;

namespace WebGalery.Core.Tests
{
    public static class MaintenanceTestData
    {
        public static DatabaseInfoEntity CreateTestDatabase()
        {
            var builder = DatabaseInfoTestDataBuilder.CreateDefault()
                .Add(RackTestDataBuilder.CreateDefault()
                    .Add(MountPointTestDataBuilder.CreateWindowsDefault())
                    .Add(MountPointTestDataBuilder.CreateLinuxDefault())
                    )
                .Add(RackTestDataBuilder.CreateDefault()
                    .Add(MountPointTestDataBuilder.CreateWindowsDefault().WithPath(@"D:\TEMP"))
                    );
            return builder.Build();
        }
    }
}
