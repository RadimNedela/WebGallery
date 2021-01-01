//using Infrastructure.Databases;
//using System.Collections.Generic;
//using System.Linq;
//using WebGalery.Core.DbEntities.Contents;
//using WebGalery.Core.DbEntities.Maintenance;

//namespace WebGalery.PictureViewer.Tests
//{
//    public class TestDatabase : IGaleryReadDatabase
//    {
//        public const string Content1Hash = "Test Content Entity 01 Hash";

//        public IList<ContentEntity> Contents { get; } = new List<ContentEntity>();
//        public IList<BinderEntity> Binders { get; } = new List<BinderEntity>();
//        public IList<DatabaseInfoEntity> DatabaseInfo { get; } = new List<DatabaseInfoEntity>();

//        public RackEntity ActiveRack { get; private set; }

//        IEnumerable<ContentEntity> IGaleryReadDatabase.Contents => Contents;

//        IEnumerable<BinderEntity> IGaleryReadDatabase.Binders => Binders;

//        IEnumerable<DatabaseInfoEntity> IGaleryReadDatabase.DatabaseInfo => DatabaseInfo;

//        public TestDatabase()
//        {
//            Initialize();
//        }

//        private void Initialize()
//        {
//            CreateDatabaseInfo();
//            AddBinder();
//            AddContent();
//        }

//        private void CreateDatabaseInfo()
//        {
//            DatabaseInfoEntity database = new()
//            {
//                Hash = "TestDatabaseInfoHash",
//                Name = "Test Database Name",
//                Racks = new List<RackEntity>()
//            };

//            RackEntity rack1 = new()
//            {
//                Hash = "Rack 01 Hash",
//                Name = "Rack 01 Name",
//                Database = database,
//                MountPoints = new List<MountPointEntity>()
//            };

//            rack1.MountPoints.Add(new MountPointEntity()
//            {
//                Path = @"d:\Sources\WebGalery\TestPictures\",
//                Rack = rack1,
//                RackHash = rack1.Hash
//            });
//            rack1.MountPoints.Add(new MountPointEntity()
//            {
//                Path = @"\home\Radim\Sources\WebGalery\TestPictures\",
//                Rack = rack1,
//                RackHash = rack1.Hash
//            }
//            );

//            DatabaseInfo.Add(database);
//            ActiveRack = rack1;
//        }

//        private void AddBinder()
//        {
//            BinderEntity binder = new()
//            {
//                Hash = "Binder 01 Hash",
//                Type = BinderTypeEnum.DirectoryType.ToString(),
//                Label = "Binder 01 Label",
//                Rack = ActiveRack,
//                RackHash = ActiveRack.Hash,
//                Contents = new List<BinderEntityToContentEntity>(),
//                AttributedContents = new List<AttributedBinderEntityToContentEntity>()
//            };

//            Binders.Add(binder);
//        }

//        private void AddContent()
//        {
//            ContentEntity content = new()
//            {
//                Hash = Content1Hash,
//                Type = ContentTypeEnum.ImageType.ToString(),
//                Label = "Test Content Entity 01 Label",
//                Binders = new List<BinderEntityToContentEntity>(),
//                AttributedBinders = new List<AttributedBinderEntityToContentEntity>()
//            };

//            var bToc = new BinderEntityToContentEntity
//            {
//                Binder = Binders.First(),
//                BinderHash = Binders.First().Hash,
//                Content = content,
//                ContentHash = content.Hash
//            };

//            content.Binders.Add(bToc);
//            Binders.First().Contents.Add(bToc);

//            var aBToC = new AttributedBinderEntityToContentEntity
//            {
//                Binder = Binders.First(),
//                BinderHash = Binders.First().Hash,
//                Content = content,
//                ContentHash = content.Hash,
//                Attribute = @"Duha\2017-08-20-Duha0367_small.jpg"
//            };

//            content.AttributedBinders.Add(aBToC);
//            Binders.First().AttributedContents.Add(aBToC);

//            Contents.Add(content);
//        }
//    }
//}
