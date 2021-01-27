using System.Linq;
using System.Reflection;
using NUnit.Framework;
using WebGalery.Core.Logging;

namespace FileImportTests.Domain
{
    [TestFixture]
    public class RackInfoBuilderTests
    {
        private static readonly ISimpleLogger Log = new MyOwnLog4NetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public const string TestPicturesPath = @"../../../../TestPictures";
        public const string TestPicturesInnerPath = TestPicturesPath + @"/Duha";
        public const string DoubledPictureName1 = "2017-08-20-Duha0383.JPG";
        public const string DoubledPictureName2 = "2017-08-20-Duha0383_2.JPG";



        [Test]
        public void GetCurrentRackInfo_ReturnsRackInfo()
        {
            var application = new FileImportTestData().CreateTestRackInfoBuilder();

            var rackInfo = application.GetCurrentRackInfo();

            Assert.That(rackInfo, Is.Not.Null);
        }

        [Test]
        public void GetCurrentRackInfo_DirectoryInfo_ReturnsInfo()
        {
            var application = new FileImportTestData().CreateTestRackInfoBuilder();

            var rackInfo = application.GetCurrentRackInfo();

            Assert.That(rackInfo.DirectoryInfo, Is.Not.Null);
            Assert.That(rackInfo.DirectoryInfo.SubDirectories, Is.Not.Empty);
            Assert.That(rackInfo.DirectoryInfo.SubDirectories.First(), Does.StartWith("TestSubDir"));
        }

        [Test]
        public void GetSubDirectoryInfo_TestSubDir_ReturnsAlsoParentDir()
        {
            var application = new FileImportTestData().CreateTestRackInfoBuilder();

            var dirInfo = application.GetSubDirectoryInfo("TestSubDir");

            Assert.That(dirInfo.SubDirectories, Is.Not.Empty);
            Assert.That(dirInfo.SubDirectories.First(), Does.EndWith(".."));
            Assert.That(dirInfo.SubDirectories.Last(), Does.StartWith("TestSubDir"));
        }

        //[Test]
        //public void GetDirectoryContent_ValidTestPath_Returns0Files()
        //{
        //    var application = GetTestApplication();
        //    var content = application.GetDirectoryContent(TestPicturesPath);
        //    Assert.That(content.ContentInfos.Count, Is.EqualTo(0));
        //}

        //[Test]
        //public void GetRecursivelyDirectoryContent_ReturnsAll8Files()
        //{
        //    var list = RecurseIntoDirectories();
        //    Assert.That(list.Count(), Is.EqualTo(8));
        //}

        //[Test]
        //public void SameTestPictures_WithDifferentMetadata_ReturnsSameHash()
        //{
        //    Stopwatch t = new Stopwatch();
        //    t.Start();
        //    using (var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider())
        //    {
        //        var directoryMethods = serviceProvider.GetService<IDirectoryMethods>();
        //        var currPath = directoryMethods.GetCurrentDirectoryName();

        //        var directoryContentBuilder = serviceProvider.GetService<DirectoryContentBuilder>();
        //        var picsPath = Path.Combine(currPath, TestPicturesInnerPath);
        //        var binder = directoryContentBuilder.GetDirectoryContent(new DirectoryContentThreadInfo { FullPath = picsPath });
        //        var first = binder.Contents.First(he => he.Label.EndsWith(DoubledPictureName1));

        //        Assert.That(first.AttributedBinders.Count(), Is.EqualTo(2));

        //        Console.WriteLine($"The hash is {first.Hash}");
        //        string s = "";
        //        foreach (var dto in binder.Contents)
        //        {
        //            s += ", " + dto.Hash;
        //        }
        //        Console.WriteLine(s);
        //    }
        //    t.Stop();
        //    Console.WriteLine($"TEst trval {t.Elapsed}");
        //}

        //private IEnumerable<ContentInfoDto> RecurseIntoDirectories()
        //{
        //    var application = GetTestApplication();
        //    var content = application.GetDirectoryContent(TestPicturesPath);
        //    IEnumerable<ContentInfoDto> list = new List<ContentInfoDto>();
        //    foreach (var dir in content.Binders)
        //    {
        //        list = list.Union(application.GetDirectoryContent(dir.Label).ContentInfos);
        //    }
        //    return list;
        //}

        //[Test]
        //public void TryIncludingDb()
        //{
        //    using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
        //    var directoryMethods = serviceProvider.GetService<IDirectoryMethods>();
        //    var currPath = directoryMethods.GetCurrentDirectoryName();

        //    var application = serviceProvider.GetService<DirectoryContentApplication>();
        //    var contentRepository = serviceProvider.GetService<IContentEntityRepository>();
        //    var binderRepository = serviceProvider.GetService<IBinderEntityRepository>();

        //    var picsPath = Path.Combine(currPath, TestPicturesInnerPath);
        //    var directoryContentDto = application.GetDirectoryContent(picsPath);

        //    foreach (var contentInfo in directoryContentDto.ContentInfos)
        //    {
        //        var contentEntity = contentRepository.Get(contentInfo.Hash);
        //        contentRepository.Remove(contentEntity);
        //    }
        //    var binderEntity = binderRepository.Get(directoryContentDto.TheBinder.Hash);
        //    binderRepository.Remove(binderEntity);

        //    contentRepository.Save();


        //    Log.Error("Právì jsem zavolal Save na content repository...");
        //    //Thread.Sleep(2000);
        //    //Log.Error("jsem za sleep");

        //    var dbStorage = serviceProvider.GetService<IDatabaseInfoElementRepository>();
        //    var defaultDb = dbStorage.First(db => db.Name == "Default");
        //    var db = serviceProvider.GetService<IGaleryDatabase>();
        //    db.DatabaseInfo.Remove(defaultDb.Entity);
        //    Log.Error("Další je zavolat SaveChanges na database infu...");
        //    db.SaveChanges();
        //}
        //[Test]
        //public void GetFileStream_ValidHashFromVisitedDirectory_ReturnsCorrectFileStream()
        //{
        //    var dirApp = GetTestApplication();
        //    var dc = dirApp.GetDirectoryContent(TestPicturesInnerPath);
        //    var hash = dc.ContentInfos.First(c => c.FilePath.EndsWith(DoubledPictureName2)).Hash;

        //    var application = serviceProvider.GetService<PhysicalFileApplication>();
        //    var content = application.GetContent(hash);
        //    Assert.That(content, Is.Not.Null);
        //    Assert.That(content.FilePath, Is.Not.Null);
        //}
    }
}