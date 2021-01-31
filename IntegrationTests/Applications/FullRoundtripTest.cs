using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.FileImport.Application;
using WebGalery.Infrastructure.Databases;
using WebGalery.IntegrationTests.IoC;
using WebGalery.Maintenance.Applications;
using WebGalery.SessionHandling.Applications;

namespace WebGalery.IntegrationTests.Applications
{
    [TestFixture]
    public class FullRoundtripTest
    {
        public const string TestDBName = "Test Database";
        public const string TestRackName = "Test Rack";

        public const string TestPicturesDirectory = @"c:\Source\WebGallery\TestPictures\";
        public const string TestPicturesSubDirectory1 = "Duha";

        [Test]
        public void ParseDirectoryContent_FullTest()
        {
            using var serviceProvider = InitializationHelper.CreateServiceCollection().BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();

            bool dbEmpty = false, dbInfoAdded = false, directoryParsed = false, dbCachesCleared = false, 
                dbContentChecked = false, dbContentDeleted = false;
            dbEmpty = CheckDBEmpty(serviceProvider);
            if (dbEmpty)
            {
                dbInfoAdded = AddDbInfo(serviceProvider);
                directoryParsed = ParseDirectory(serviceProvider);
            }
            dbCachesCleared = ClearDBCaches(serviceProvider);
            dbContentChecked = CheckDBContent(serviceProvider);
            dbContentDeleted = DeleteDBContent(serviceProvider);

            Assert.That(dbEmpty, "DB not empty");
            Assert.That(dbInfoAdded, "DB info not added");
            Assert.That(directoryParsed, "Directory not parsed");
            Assert.That(dbCachesCleared, "Db caches not cleared");
            Assert.That(dbContentChecked, "DB content not checked");
            Assert.That(dbContentDeleted, "db content not deleted");
        }

        private bool CheckDBEmpty(ServiceProvider serviceProvider)
        {
            var dbInfoRepository = serviceProvider.GetService<IDatabaseInfoEntityRepository>();
            var binderRepository = serviceProvider.GetService<IBinderEntityRepository>();
            var contentRepository = serviceProvider.GetService<IContentEntityRepository>();

            var infos = dbInfoRepository.GetAll().Where(db => db.Name == TestDBName);
            return !infos.Any();
        }

        private bool AddDbInfo(ServiceProvider serviceProvider)
        {
            var dbInfoApplication = serviceProvider.GetService<DatabaseInfoApplication>();
            var dbInfo = dbInfoApplication.CreateNewDatabase(TestDBName);
            dbInfo.Racks.First().Name = TestRackName;
            dbInfo.Racks.First().MountPoints[0] = TestPicturesDirectory;
            dbInfoApplication.UpdateDatabaseNames(dbInfo);

            var dbInfoInitializer = serviceProvider.GetService<IDatabaseInfoInitializer>();
            dbInfoInitializer.SetCurrentInfo(dbInfo.Hash, dbInfo.Racks.First().Hash);
            return true;
        }

        private bool ParseDirectory(ServiceProvider serviceProvider)
        {
            var fileImportApplication = serviceProvider.GetService<FileImportApplication>();
            var threadInfoDto = fileImportApplication.ParseDirectoryContent(TestPicturesSubDirectory1);

            return threadInfoDto.FilesDone == 5;
        }

        private bool ClearDBCaches(ServiceProvider serviceProvider)
        {
            var galeryDatabase = serviceProvider.GetService<IGaleryDatabase>();
            galeryDatabase.DetachAllEntities();
            return true;
        }

        private bool CheckDBContent(ServiceProvider serviceProvider)
        {
            return false;
        }

        private bool DeleteDBContent(ServiceProvider serviceProvider)
        {
            var dbInfoApplication = serviceProvider.GetService<DatabaseInfoApplication>();
            var hash = dbInfoApplication.GetAllDatabases().First(db => db.Name == TestDBName).Hash;
            dbInfoApplication.DeleteDatabase(hash);
            return true;
        }
    }
}
