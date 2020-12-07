using NUnit.Framework;

namespace IntegrationTests.DBTests
{
    [TestFixture]
    public class DatabaseMaintenaceTests
    {
        //[SetUp]
        //public void SetUp()
        //{
        //    using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
        //    var databaseApplication = serviceProvider.GetService<DatabaseInfoApplication>();
        //    _dto = databaseApplication.CreateNewDatabase("TestDatabase");

        //    databaseApplication.AddNewRack(_dto.Hash, "NewTestRack", "/mount/ExternalStorage001/Something");
        //}

        //[TearDown]
        //public void TearDown()
        //{
        //    if (_dto != null)
        //    {
        //        using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
        //        var getter = serviceProvider.GetService<IDatabaseInfoEntityRepository>();
        //        var entity = getter.Get(_dto.Hash);

        //        var database = serviceProvider.GetService<IGaleryDatabase>();
        //        database.DatabaseInfo.Remove(entity);
        //        database.SaveChanges();
        //    }
        //}

        //[Test]
        //public void CreatedTestMasterData_AreCorrectlyWrittenToDB()
        //{
        //    using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
        //    var database = serviceProvider.GetService<IDatabaseInfoEntityRepository>();
        //    var dbInfo = database.Get(_dto.Hash);

        //    Assert.That(dbInfo, Is.Not.Null);
        //    Assert.That(dbInfo.Name, Is.EqualTo("TestDatabase"));
        //    var testRack = dbInfo.Racks.First(r => r.Name == "NewTestRack");
        //    Assert.That(testRack.Name, Is.EqualTo("NewTestRack"));
        //    Assert.That(testRack.MountPoints.First().Path, Does.Contain("ExternalStorage001"));
        //}
    }
}
