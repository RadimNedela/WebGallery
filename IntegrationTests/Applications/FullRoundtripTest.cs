using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebGalery.Application.FileImport;
using WebGalery.Application.Maintenance;
using WebGalery.Core;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Infrastructure.Databases;
using WebGalery.IntegrationTests.IoC;

namespace WebGalery.IntegrationTests.Applications
{
    [TestFixture]
    public class FullRoundtripTest
    {
        public const string TestDbName = "Test Database";
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
            dbEmpty = CheckDbEmpty(serviceProvider);
            if (dbEmpty)
            {
                dbInfoAdded = AddDbInfo(serviceProvider);
                directoryParsed = ParseDirectory(serviceProvider);
            }
            dbCachesCleared = ClearDbCaches(serviceProvider);
            dbContentChecked = CheckDbContent(serviceProvider);
            dbContentDeleted = DeleteDbContent(serviceProvider);

            Assert.That(dbEmpty, "DB not empty");
            Assert.That(dbInfoAdded, "DB info not added");
            Assert.That(directoryParsed, "Directory not parsed");
            Assert.That(dbCachesCleared, "Db caches not cleared");
            Assert.That(dbContentChecked, "DB content not checked");
            Assert.That(dbContentDeleted, "db content not deleted");
        }

        private bool CheckDbEmpty(ServiceProvider serviceProvider)
        {
            var dbInfoRepository = serviceProvider.GetService<IDatabaseInfoRepository>();
            var binderRepository = serviceProvider.GetService<IBinderRepository>();
            var contentRepository = serviceProvider.GetService<IContentEntityRepository>();

            var infos = dbInfoRepository.GetAll().Where(db => db.Name == TestDbName);
            return !infos.Any();
        }

        private bool AddDbInfo(ServiceProvider serviceProvider)
        {
            var dbInfoApplication = serviceProvider.GetService<DatabaseInfoApplication>();
            var dbInfo = dbInfoApplication.CreateNewDatabase(TestDbName);
            dbInfo.Racks.First().Name = TestRackName;
            dbInfo.Racks.First().MountPoints[0] = TestPicturesDirectory;
            dbInfoApplication.UpdateDatabaseNames(dbInfo);

            var dbInfoInitializer = serviceProvider.GetService<IGalerySessionInitializer>();
            dbInfoInitializer.SetCurrentInfo(dbInfo.Hash, dbInfo.Racks.First().Hash);
            return true;
        }

        private bool ParseDirectory(ServiceProvider serviceProvider)
        {
            var fileImportApplication = serviceProvider.GetService<FileImportApplication>();
            var threadInfoDto = fileImportApplication.ParseDirectoryContent(TestPicturesSubDirectory1);

            return threadInfoDto.FilesDone == 5;
        }

        private bool ClearDbCaches(ServiceProvider serviceProvider)
        {
            var galeryDatabase = serviceProvider.GetService<IGaleryDatabase>();
            galeryDatabase.DetachAllEntities();
            return true;
        }

        private bool CheckDbContent(ServiceProvider serviceProvider)
        {
            return false;
        }

        private bool DeleteDbContent(ServiceProvider serviceProvider)
        {
            var dbInfoApplication = serviceProvider.GetService<DatabaseInfoApplication>();
            var hash = dbInfoApplication.GetAllDatabases().First(db => db.Name == TestDbName).Hash;
            dbInfoApplication.DeleteDatabase(hash);
            return true;
        }
    }
}
