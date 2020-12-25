using WebGalery.Core.DbEntities.Maintenance;

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
                .Add(RackTestDataBuilder.CreateDefault().WithName("Second Test Rack")
                    .Add(MountPointTestDataBuilder.CreateWindowsDefault().WithPath(@"D:\TEMP"))
                    );
            return builder.Build();
        }
    }
}
